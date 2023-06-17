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

namespace WMS.API.Services.OrderControllers;

public class OrderService : IDocumentRepository<OrderDto>
{
    private readonly IDocumentNumeratorService<Order> _documentNumeratorService;
    private readonly ApplicationDbContext _context;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IdentityHelperService _identityHelperService;
    private readonly IMapper _mapper;
    private readonly IUserNotificationService _userNotificationService;

    public OrderService(ApplicationDbContext context, IMapper mapper,
        IUserNotificationService userNotificationService,
        IdentityHelperService identityHelperService, IDocumentNumeratorService<Order> documentNumeratorService, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _context = context;
        _mapper = mapper;
        _userNotificationService = userNotificationService;
        _identityHelperService = identityHelperService;
        _documentNumeratorService = documentNumeratorService;
        _dbContextFactory = dbContextFactory;
    }


    public async Task<OrderDto> Create(OrderDto itemDto, CancellationToken cancellationToken)
    {
        var newGuid = Guid.NewGuid();
        var item = _mapper.Map<Order>(itemDto);
        item.UniqueCode = await _documentNumeratorService.SetCatalogNumber(item.UniqueCode);
        item.Id = newGuid;
        foreach (var order in item.OrderDetails)
        {
            order.Id = Guid.NewGuid();
            order.OrderId = newGuid;
            var acceptanceOfGood = await _context.AcceptanceOfGoods.FirstOrDefaultAsync(x => x.ProductId == order.ProductId, cancellationToken);
            if (acceptanceOfGood == null)
                throw new Exception("Product quantity empty");
            acceptanceOfGood.Qty -= (int)order.Quantity;
            _context.AcceptanceOfGoods.Update(acceptanceOfGood);
        }
        _context.Add(item);
        await _context.SaveChangesAsync(cancellationToken);
        var request = _mapper.Map<OrderDto>(item);
        return request;
    }
    public async Task<IEnumerable<OrderDto>> GetAll(
        CancellationToken cancellationToken,
        Expression<Func<OrderDto, bool>>? whereClause = null,
        Expression<Func<OrderDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Order>().AsQueryable();
        var dtos = queryable.ProjectTo<OrderDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.ToListAsync(cancellationToken);
    }
    public async Task<OrderDto?> Get(Guid id, CancellationToken cancellationToken,
        params Expression<Func<OrderDto, object>>[] includeExpressions)
    {
        var item = await _context.Set<Order>()
            .AsQueryable()
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return item;
    }
    public async Task<OrderDto> Update(OrderDto dto, CancellationToken cancellationToken)
    {
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var item = await context.Orders
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (item == null) throw new DocumentNotFoundException(dto.Id);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        _mapper.Map(dto, item);
        context.Entry(item).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        var result = _mapper.Map<OrderDto>(item);
        return result;
    }
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<Order>()
            .FirstOrDefaultAsync(x => ((BaseCatalog)x).Id == id, cancellationToken);
        if (entity != null)
            _context.Set<Order>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<OrderDto>> GetPage(CancellationToken cancellationToken,
        int PaneNo,
        int PageSize,
        Expression<Func<OrderDto, bool>>? whereClause = null,
        Expression<Func<OrderDto, string>>? orderClause = null)
    {
        var queryable = _context.Set<Order>().AsQueryable();
        var dtos = queryable.ProjectTo<OrderDto>(_mapper.ConfigurationProvider);

        if (whereClause is not null) dtos = dtos.Where(whereClause);

        if (orderClause is not null) dtos = dtos.OrderBy(orderClause);

        return await dtos.Skip((PaneNo - 1) * PageSize).Take(PageSize).ToListAsync(cancellationToken);
    }
}