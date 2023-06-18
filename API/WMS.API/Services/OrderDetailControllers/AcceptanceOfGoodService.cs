using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WMS.API.Services.Helpers;
using WMS.Data.Context;
using WMS.Data.DTO.OrderDtos;
using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Orders;
using WMS.Data.Interface;
using WMS.Data.Interface.ControllerInterface;
using WMS.Data.Middlewares.CustomExceptions;

namespace WMS.API.Services.OrderDetailControllers;

public class OrderDetailService : IDocumentRepository<OrderDetailDto>
{
    private readonly IDocumentNumeratorService<OrderDetail> _documentNumeratorService;
    private readonly ApplicationDbContext _context;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IdentityHelperService _identityHelperService;
    private readonly IMapper _mapper;
    private readonly IUserNotificationService _userNotificationService;

    public OrderDetailService(ApplicationDbContext context, IMapper mapper,
        IUserNotificationService userNotificationService,
        IdentityHelperService identityHelperService, IDocumentNumeratorService<OrderDetail> documentNumeratorService, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _context = context;
        _mapper = mapper;
        _userNotificationService = userNotificationService;
        _identityHelperService = identityHelperService;
        _documentNumeratorService = documentNumeratorService;
        _dbContextFactory = dbContextFactory;
    }


    public async Task<OrderDetailDto> Create(OrderDetailDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<OrderDetail>(itemDto);
        _context.Set<OrderDetail>().Add(item);
        await _context.SaveChangesAsync();
        var request = _mapper.Map<OrderDetailDto>(item);
        return request;
    }
    public async Task<IEnumerable<OrderDetailDto>> GetAll(
        CancellationToken cancellationToken,
        Expression<Func<OrderDetailDto, bool>>? whereClause = null,
        Expression<Func<OrderDetailDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<OrderDetail>().AsQueryable();
        var dtos = queryable.ProjectTo<OrderDetailDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.ToListAsync(cancellationToken);
    }
    public async Task<OrderDetailDto?> Get(Guid id, CancellationToken cancellationToken,
        params Expression<Func<OrderDetailDto, object>>[] includeExpressions)
    {
        var item = await _context.Set<OrderDetail>()
            .AsQueryable()
            .ProjectTo<OrderDetailDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return item;
    }
    public async Task<OrderDetailDto> Update(OrderDetailDto dto, CancellationToken cancellationToken)
    {
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var item = await context.OrderDetails
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (item == null) throw new DocumentNotFoundException(dto.Id);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        _mapper.Map(dto, item);
        context.Entry(item).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        var result = _mapper.Map<OrderDetailDto>(item);
        return result;
    }
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<OrderDetail>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity != null)
            _context.Set<OrderDetail>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<OrderDetailDto>> GetPage(CancellationToken cancellationToken,
        int PaneNo,
        int PageSize,
        Expression<Func<OrderDetailDto, bool>>? whereClause = null,
        Expression<Func<OrderDetailDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<OrderDetail>().AsQueryable();
        var dtos = queryable.ProjectTo<OrderDetailDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.Skip((PaneNo - 1) * PageSize).Take(PageSize).ToListAsync(cancellationToken);
    }
}