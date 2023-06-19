using WMS.Data.DTO.BaseClassDtos;

namespace WMS.Data.DTO.PositionDtos;

public class PositionDto : BaseCatalogDto
{
    public string? Description { get; set; }
    public DateTime DateOfApproval { get; set; }
    public bool PositionApproved { get; set; }
    public decimal MainSalary { get; set; }
    public Guid? DivisionId { get; set; }
    public string DivisionName { get; set; } = string.Empty;
    public Guid? CompanyId { get; set; }
    public Guid? MainWorkScheduleId { get; set; }
}