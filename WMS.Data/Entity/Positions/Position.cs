using WMS.Data.DTO.DivisionDtos;
using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Divisions;

namespace WMS.Data.Entity.Positions;

public class Position : BaseCatalog
{
    public string? Description { get; set; }
    public DateTime DateOfApproval { get; set; }
    public bool PositionApproved { get; set; }
    public decimal MainSalary { get; set; }
    public Guid? DivisionId { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? MainWorkScheduleId { get; set; }
    public Division? Division { get; set; }
}