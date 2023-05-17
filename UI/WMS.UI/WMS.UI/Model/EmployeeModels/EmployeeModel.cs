using WMS.UI.Constant.Enum;
using WMS.UI.Model.BaseClassModels;

namespace WMS.UI.Model.EmployeeModels;

public class EmployeeModel : BaseCatalogModel
{
    public Guid? DivisionId { get; set; }
    public Guid? PositionId { get; set; }
    public Guid? PersonId { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string? Tin { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public Guid? VendorCustomerId { get; set; }
}