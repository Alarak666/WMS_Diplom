using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WMS.API.Services.Helpers;
using WMS.Data.Context;
using WMS.Data.DTO.DivisionDtos;
using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Divisions;
using WMS.Data.Interface;
using WMS.Data.Interface.ControllerInterface;
using WMS.Data.Middlewares.CustomExceptions;

namespace WMS.API.Services.DivisionServices;

public class DivisionService : IDocumentRepository<DivisionDto>
{
    private readonly IDocumentNumeratorService<Division> _documentNumeratorService;
    private readonly ApplicationDbContext _context;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IdentityHelperService _identityHelperService;
    private readonly IMapper _mapper;
    private readonly IUserNotificationService _userNotificationService;

    public DivisionService(ApplicationDbContext context, IMapper mapper,
        IUserNotificationService userNotificationService,
        IdentityHelperService identityHelperService, IDocumentNumeratorService<Division> documentNumeratorService, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _context = context;
        _mapper = mapper;
        _userNotificationService = userNotificationService;
        _identityHelperService = identityHelperService;
        _documentNumeratorService = documentNumeratorService;
        _dbContextFactory = dbContextFactory;
    }


    public async Task<DivisionDto> Create(DivisionDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Division>(itemDto);
        item.UniqueCode = await _documentNumeratorService.SetCatalogNumber(item.UniqueCode);
        _context.Set<Division>().Add(item);
        await _context.SaveChangesAsync();
        var request = _mapper.Map<DivisionDto>(item);
        return request;
    }
    public async Task<IEnumerable<DivisionDto>> GetAll(
        CancellationToken cancellationToken,
        Expression<Func<DivisionDto, bool>>? whereClause = null,
        Expression<Func<DivisionDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Division>().AsQueryable();
        var dtos = queryable.ProjectTo<DivisionDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.ToListAsync(cancellationToken);
    }
    public async Task<DivisionDto?> Get(Guid id, CancellationToken cancellationToken,
        params Expression<Func<DivisionDto, object>>[] includeExpressions)
    {
        var item = await _context.Set<Division>()
            .AsQueryable()
            .ProjectTo<DivisionDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return item;
    }
    public async Task<DivisionDto> Update(DivisionDto dto, CancellationToken cancellationToken)
    {
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var item = await context.Divisions
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (item == null) throw new DocumentNotFoundException(dto.Id);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        _mapper.Map(dto, item);
        context.Entry(item).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        var result = _mapper.Map<DivisionDto>(item);
        return result;
    }
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<Division>()
            .FirstOrDefaultAsync(x => ((BaseCatalog)x).Id == id, cancellationToken);
        if (entity != null)
            _context.Set<Division>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<DivisionDto>> GetPage(CancellationToken cancellationToken,
        int PaneNo,
        int PageSize,
        Expression<Func<DivisionDto, bool>>? whereClause = null,
        Expression<Func<DivisionDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Division>().AsQueryable();
        var dtos = queryable.ProjectTo<DivisionDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.Skip((PaneNo - 1) * PageSize).Take(PageSize).ToListAsync(cancellationToken);
    }
}