using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMS.Data.Context;

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


}