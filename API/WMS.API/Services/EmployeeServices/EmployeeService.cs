using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WMS.API.Services.Helpers;
using WMS.Data.Context;
using WMS.Data.DTO.EmployeeDtos;
using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Employees;
using WMS.Data.Interface;
using WMS.Data.Interface.ControllerInterface;
using WMS.Data.Middlewares.CustomExceptions;

namespace WMS.API.Services.EmployeeServices;

public class EmployeeService : IDocumentRepository<EmployeeDto>
{
    private readonly IDocumentNumeratorService<Employee> _documentNumeratorService;
    private readonly ApplicationDbContext _context;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IdentityHelperService _identityHelperService;
    private readonly IMapper _mapper;
    private readonly IUserNotificationService _userNotificationService;

    public EmployeeService(ApplicationDbContext context, IMapper mapper,
        IUserNotificationService userNotificationService,
        IdentityHelperService identityHelperService, IDocumentNumeratorService<Employee> documentNumeratorService, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _context = context;
        _mapper = mapper;
        _userNotificationService = userNotificationService;
        _identityHelperService = identityHelperService;
        _documentNumeratorService = documentNumeratorService;
        _dbContextFactory = dbContextFactory;
    }


    public async Task<EmployeeDto> Create(EmployeeDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Employee>(itemDto);
        item.UniqueCode = await _documentNumeratorService.SetCatalogNumber(item.UniqueCode);
        _context.Set<Employee>().Add(item);
        await _context.SaveChangesAsync();
        var request = _mapper.Map<EmployeeDto>(item);
        return request;
    }
    public async Task<IEnumerable<EmployeeDto>> GetAll(
        CancellationToken cancellationToken,
        Expression<Func<EmployeeDto, bool>>? whereClause = null,
        Expression<Func<EmployeeDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Employee>().Include(x => x.Position).AsQueryable();
        var dtos = queryable.ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);
        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.ToListAsync(cancellationToken);
    }
    public async Task<EmployeeDto?> Get(Guid id, CancellationToken cancellationToken,
        params Expression<Func<EmployeeDto, object>>[] includeExpressions)
    {
        var item = await _context.Set<Employee>()
            .AsQueryable()
            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return item;
    }
    public async Task<EmployeeDto> Update(EmployeeDto dto, CancellationToken cancellationToken)
    {
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var item = await context.Employees
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (item == null) throw new DocumentNotFoundException(dto.Id);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        _mapper.Map(dto, item);
        context.Entry(item).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        var result = _mapper.Map<EmployeeDto>(item);
        return result;
    }
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<Employee>()
            .FirstOrDefaultAsync(x => ((BaseCatalog)x).Id == id, cancellationToken);
        if (entity != null)
            _context.Set<Employee>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<EmployeeDto>> GetPage(CancellationToken cancellationToken,
        int PaneNo,
        int PageSize,
        Expression<Func<EmployeeDto, bool>>? whereClause = null,
        Expression<Func<EmployeeDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Employee>().AsQueryable();
        var dtos = queryable.ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);
        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.Skip((PaneNo - 1) * PageSize).Take(PageSize).ToListAsync(cancellationToken);
    }
}