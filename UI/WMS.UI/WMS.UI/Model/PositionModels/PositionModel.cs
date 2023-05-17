using WMS.UI.Model.BaseClassModels;

namespace WMS.UI.Model.PositionModels;

public class PositionModel : BaseCatalogModel
{
    public string? Description { get; set; }
    public DateTime DateOfApproval { get; set; }
    public bool PositionApproved { get; set; }
    public decimal MainSalary { get; set; }
    public Guid? DivisionId { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? MainWorkScheduleId { get; set; }
}