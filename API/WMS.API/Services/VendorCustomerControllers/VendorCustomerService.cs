using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WMS.API.Services.Helpers;
using WMS.Data.Context;
using WMS.Data.DTO.VendorCustomerDtos;
using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.VendorCustomers;
using WMS.Data.Interface;
using WMS.Data.Interface.ControllerInterface;
using WMS.Data.Middlewares.CustomExceptions;

namespace WMS.API.Services.VendorCustomerControllers;

public class VendorCustomerService : IDocumentRepository<VendorCustomerDto>
{
    private readonly IDocumentNumeratorService<VendorCustomer> _documentNumeratorService;
    private readonly ApplicationDbContext _context;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IdentityHelperService _identityHelperService;
    private readonly IMapper _mapper;
    private readonly IUserNotificationService _userNotificationService;

    public VendorCustomerService(ApplicationDbContext context, IMapper mapper,
        IUserNotificationService userNotificationService,
        IdentityHelperService identityHelperService, IDocumentNumeratorService<VendorCustomer> documentNumeratorService, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _context = context;
        _mapper = mapper;
        _userNotificationService = userNotificationService;
        _identityHelperService = identityHelperService;
        _documentNumeratorService = documentNumeratorService;
        _dbContextFactory = dbContextFactory;
    }


    public async Task<VendorCustomerDto> Create(VendorCustomerDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<VendorCustomer>(itemDto);
        item.UniqueCode = await _documentNumeratorService.SetCatalogNumber(item.UniqueCode);
        _context.Set<VendorCustomer>().Add(item);
        await _context.SaveChangesAsync();
        var request = _mapper.Map<VendorCustomerDto>(item);
        return request;
    }
    public async Task<IEnumerable<VendorCustomerDto>> GetAll(
        CancellationToken cancellationToken,
        Expression<Func<VendorCustomerDto, bool>>? whereClause = null,
        Expression<Func<VendorCustomerDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<VendorCustomer>().AsQueryable();
        var dtos = queryable.ProjectTo<VendorCustomerDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.ToListAsync(cancellationToken);
    }
    public async Task<VendorCustomerDto?> Get(Guid id, CancellationToken cancellationToken,
        params Expression<Func<VendorCustomerDto, object>>[] includeExpressions)
    {
        var item = await _context.Set<VendorCustomer>()
            .AsQueryable()
            .ProjectTo<VendorCustomerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return item;
    }
    public async Task<VendorCustomerDto> Update(VendorCustomerDto dto, CancellationToken cancellationToken)
    {
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var item = await context.VendorCustomers
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (item == null) throw new DocumentNotFoundException(dto.Id);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        _mapper.Map(dto, item);
        context.Entry(item).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        var result = _mapper.Map<VendorCustomerDto>(item);
        return result;
    }
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<VendorCustomer>()
            .FirstOrDefaultAsync(x => ((BaseCatalog)x).Id == id, cancellationToken);
        if (entity != null)
            _context.Set<VendorCustomer>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<VendorCustomerDto>> GetPage(CancellationToken cancellationToken,
        int PaneNo,
        int PageSize,
        Expression<Func<VendorCustomerDto, bool>>? whereClause = null,
        Expression<Func<VendorCustomerDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<VendorCustomer>().AsQueryable();
        var dtos = queryable.ProjectTo<VendorCustomerDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.Skip((PaneNo - 1) * PageSize).Take(PageSize).ToListAsync(cancellationToken);
    }
}