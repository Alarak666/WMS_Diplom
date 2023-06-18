using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMS.Data.Constant.Enum;
using WMS.Data.Context;
using WMS.Data.DTO.OrderDtos;
using WMS.Data.ReportDto;

namespace WMS.API.Controllers.ReportAndDashBoar;

[ApiController]
[Route("api/[controller]")]

public class DashboardController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public DashboardController(IMapper mapper, ApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public class DashBoardPalletDto
    {
        public string PalletName { get; set; } = string.Empty;
        public Guid? AreaTypeId { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public PalletType Type { get; set; }
        public int Quantity { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
        public int Weight { get; set; }
    }
    public class DashBoardSalesDto
    {
        public int OrderId { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DashBoardPalletDto>>> GetAll(CancellationToken cancellationToken)
    {
        var list = await _context.Pallets.Include(x => x.AreaType).ToListAsync(cancellationToken);
        var listpallet = new List<DashBoardPalletDto>();
        foreach (var item in list)
        {
            listpallet.Add(new DashBoardPalletDto
            {
                PalletName = item.Name,
                AreaTypeId = item.AreaTypeId,
                AreaName = item.AreaType?.Name ?? "",
                Type = item.Type,
                Quantity = item.Quantity,
                Width = item.Width,
                Height = item.Height,
                Length = item.Length,
                Weight = item.Weight
            });
        }
        return Ok(listpallet);
    }

    [HttpGet("employee-report")]
    public async Task<ActionResult<IEnumerable<EmployeeReportDto>>> EmploeeReport(CancellationToken cancellationToken)
    {
        var list = await _context.Employees
            .Include(x => x.Person)
            .Include(x => x.Division)
            .Include(x => x.Position)
            .Include(x => x.VendorCustomer)
            .ToListAsync(cancellationToken);
        var listpallet = new List<EmployeeReportDto>();
        foreach (var item in list)
        {
            listpallet.Add(new EmployeeReportDto
            {
                DivisionId = item.DivisionId,
                DivisionName = item.Division?.Name,
                PositionId = item.PositionId,
                PositionName = item.Position?.Name,
                HireDate = item.HireDate,
                DepartureDate = item.DepartureDate,
                DateOfBirth = item.DateOfBirth,
                Gender = item.Gender,
                Tin = item.Tin,
                LastName = item.LastName,
                FirstName = item.FirstName,
                MiddleName = item.MiddleName
            });
        }
        return Ok(listpallet);
    }

    [HttpGet("acceptance-of-good-report")]
    public async Task<ActionResult<IEnumerable<AcceptanceofGoodReportDto>>> AcceptanceofGoodReport(CancellationToken cancellationToken)
    {
        var list = await _context.AcceptanceOfGoods
            .Include(x => x.Employer)
            .Include(x => x.Product)
            .Include(x => x.TypePallet)
            .ToListAsync(cancellationToken);
        var listpallet = new List<AcceptanceofGoodReportDto>();
        foreach (var item in list)
        {
            listpallet.Add(new AcceptanceofGoodReportDto
            {
                TypePalletId = item.TypePalletId,
                ProductId = item.ProductId,
                EmployerId = item.EmployerId,
                Width = item.Width,
                Height = item.Height,
                Length = item.Length,
                Weight = item.Weight,
                Qty = item.Qty,
                DateAccepts = item.DateAccepts,
                DataExpiration = item.DataExpiration,
                NPallet = item.NPallet
            });
        }
        return Ok(listpallet);
    }
    [HttpGet("person-report")]
    public async Task<ActionResult<IEnumerable<PersonReportDto>>> PersonReport(CancellationToken cancellationToken)
    {
        var list = await _context.Persons
            .ToListAsync(cancellationToken);
        var listpallet = new List<PersonReportDto>();
        foreach (var item in list)
        {
            listpallet.Add(new PersonReportDto
            {
                Email = item.Email,
                IsEmailValidPerson = item.IsEmailValidPerson,
                PhoneNumber = item.PhoneNumber,
                ActualAddress = item.ActualAddress,
                ValidToDate = item.ValidToDate,
                IssuedDate = item.IssuedDate,
                IssuedBy = item.IssuedBy,
                DocumentNumber = item.DocumentNumber,
                DocumentSeries = item.DocumentSeries,
                DocumentType = item.DocumentType,
                AddressOfBirth = item.AddressOfBirth,
                DateOfBirth = item.DateOfBirth,
                Gender = item.Gender,
                Tin = item.Tin,
                LastName = item.LastName,
                FirstName = item.FirstName
            });
        }
        return Ok(listpallet);
    }

    [HttpGet("product-report")]
    public async Task<ActionResult<IEnumerable<ProductReportDto>>> ProductReport(CancellationToken cancellationToken)
    {
        var list = await _context.Products
            .ToListAsync(cancellationToken);
        var listpallet = new List<ProductReportDto>();
        foreach (var item in list)
        {
            listpallet.Add(new ProductReportDto
            {
                Id =item.Id ,
                CreatedDate =item.CreatedDate ,
                Name =item.Name ,
                UniqueCode =item.UniqueCode ,
                ItemType =item.ItemType ,
                Description =item.Description ,
                Barcode =item.Barcode ,
                VendorCode =item.VendorCode ,
                MainUnitName =item.Unit?.Name ?? "",
                MainUnitId =item.MainUnitId ,
                Price =item.Price ?? 0 ,
            });
        }
        return Ok(listpallet);
    }

    [HttpGet("sales-report")]
    public async Task<ActionResult<IEnumerable<SalesReportDto>>> SalesReport(CancellationToken cancellationToken)
    {
        var list = await _context.Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.Product)
            .Include(x => x.VendorCustomer)
            .Include(x => x.Employee)
            .ToListAsync(cancellationToken);
        var listpallet = new List<SalesReportDto>();
        foreach (var item in list)
        {
            listpallet.Add(new SalesReportDto
            {
                EmployeeId = item.EmployeeId,
                VendorCustomerId = item.VendorCustomerId,
                ShippingAddress = item.ShippingAddress,
                PaymentMethod = item.PaymentMethod,
                OrderStatus = item.OrderStatus,
                DateOrders = item.DateOrders,
                OrderDetails = item.OrderDetails.Select(x => new OrderDetailDto
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    ProductId = x.ProductId,
                    ProductName = x.Product?.Name ?? "",
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                })

            });
        }
        return Ok(listpallet);
    }

}