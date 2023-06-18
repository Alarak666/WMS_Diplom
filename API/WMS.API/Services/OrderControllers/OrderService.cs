using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WMS.API.Services.Helpers;
using WMS.Data.Context;
using WMS.Data.DTO.OrderDtos;
using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Orders;
using WMS.Data.Entity.Stocks;
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
        await _context.Database.BeginTransactionAsync(cancellationToken);

        item.Id = newGuid;
        var itemOrder = new Order
        {
            Id = newGuid,
            CreatedUserId = itemDto.CreatedUserId,
            CreatedDate = itemDto.CreatedDate,
            UniqueCode = itemDto.UniqueCode,
            Name = itemDto.Name,
            EmployeeId = itemDto.EmployeeId,
            VendorCustomerId = itemDto.VendorCustomerId,
            ShippingAddress = itemDto.ShippingAddress,
            PaymentMethod = itemDto.PaymentMethod,
            OrderStatus = itemDto.OrderStatus,
            DateOrders = itemDto.DateOrders,
        };
        _context.Add(itemOrder);
        await _context.SaveChangesAsync(cancellationToken);

        var orderDetails = new List<OrderDetail>();
        foreach (var order in item.OrderDetails)
        {
            order.Id = Guid.NewGuid();
            order.OrderId = newGuid;
            var acceptanceOfGood = await _context.AcceptanceOfGoods.Where(x => x.ProductId == order.ProductId).ToListAsync(cancellationToken);
            if (acceptanceOfGood == null)
                throw new Exception("Product quantity empty");
            var sum = acceptanceOfGood.Sum(x => x.Qty);
            sum -= (int)order.Quantity;
            if (sum < 0)
                throw new Exception("product quantity is less than 0");
            foreach (var x in acceptanceOfGood)
            {
                var a = x.Qty;
                sum = Math.Abs(sum-a);
                if (sum <= a)
                {
                    x.Qty -= sum;
                    sum = 0;
                    break;
                }

                x.Qty = 0;
                x.DataExpiration = new DateTime(1111, 11, 11);
            }
            _context.AcceptanceOfGoods.UpdateRange(acceptanceOfGood);
            await _context.SaveChangesAsync(cancellationToken);
            orderDetails.Add(order);
        }
        await _context.AddRangeAsync(orderDetails);
        await _context.SaveChangesAsync(cancellationToken);
        await _context.Database.CommitTransactionAsync(cancellationToken);
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
        var newGuid = Guid.NewGuid();
        var item = _mapper.Map<Order>(dto);
        await _context.Database.BeginTransactionAsync(cancellationToken);

        item.Id = newGuid;
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var order = await _context.Orders.FindAsync(dto.Id);
        if (order == null) throw new Exception($"Not found document{order.Id}");
        var itemOrder = new Order
        {
            Id = dto.Id,
            CreatedUserId = dto.CreatedUserId,
            CreatedDate = dto.CreatedDate,
            UniqueCode = dto.UniqueCode,
            Name = dto.Name,
            EmployeeId = dto.EmployeeId,
            VendorCustomerId = dto.VendorCustomerId,
            ShippingAddress = dto.ShippingAddress,
            PaymentMethod = dto.PaymentMethod,
            OrderStatus = dto.OrderStatus,
            DateOrders = dto.DateOrders,
        };
        _context.Update(itemOrder);

        var productItemsToUpdate = await _context.OrderDetails
            .Where(x => x.OrderId == item.Id).ToListAsync(cancellationToken);
        _context.RemoveRange(productItemsToUpdate);

        if (dto.OrderDetails != null)
        {
            foreach (var orderUpdate in dto.OrderDetails)
            {
                var newOrderDetail = new OrderDetail
                {
                    Id = orderUpdate.Id,
                    OrderId = orderUpdate.OrderId,
                    ProductId = orderUpdate.ProductId,
                    Quantity = orderUpdate.Quantity,
                    UnitPrice = orderUpdate.UnitPrice
                };
                if (productItemsToUpdate.Any(x => x.Id == orderUpdate.Id))
                {
                    _context.Add(newOrderDetail);
                }
                else
                {
                    newOrderDetail.OrderId = item.Id;
                    newOrderDetail.Id = Guid.NewGuid();
                    var acceptanceOfGood = await _context.AcceptanceOfGoods.Where(x => x.ProductId == orderUpdate.ProductId).ToListAsync(cancellationToken);
                    if (acceptanceOfGood == null)
                        throw new Exception("Product quantity empty");
                    var sum = acceptanceOfGood.Sum(x => x.Qty);
                    sum -= (int)orderUpdate.Quantity;
                    if (sum < 0)
                        throw new Exception("product quantity is less than 0");
                    for (int i = 0; i < acceptanceOfGood.Count; i++)
                    {
                        var x = acceptanceOfGood[i];
                        var a = x.Qty;
                        sum -= a;
                        if (sum <= a)
                        {
                            x.Qty -= sum;
                            sum = 0;
                            break;
                        }

                        x.Qty = 0;
                        x.DataExpiration = new DateTime(1111, 11, 11);
                    }
                    _context.AcceptanceOfGoods.UpdateRange(acceptanceOfGood);
                    _context.Add(newOrderDetail);
                }
            }
        }

        await context.SaveChangesAsync(cancellationToken);
        await _context.Database.CommitTransactionAsync(cancellationToken);
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