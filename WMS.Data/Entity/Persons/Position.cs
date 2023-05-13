using ERP.Core.Entities.Catalogs.CompanyCatalog;
using ERP.Core.Entities.Catalogs.WorkScheduleCatalog;
using ERP.Core.Enums;

namespace WMS.Data.Entity.Persons;

public class Position : BaseCatalog
{
    public Position()
    {
        ReferenceType = ReferenceType.CatalogPosition;
    }

    public int OrisId { get; set; }
    public string? Description { get; set; }
    public DateTime DateOfApproval { get; set; }
    public bool PositionApproved { get; set; }
    public decimal MainSalary { get; set; }
    public Guid? DivisionId { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? MainWorkScheduleId { get; set; }
    public WorkSchedule? MainWorkSchedule { get; set; }
    public Company? Company { get; set; }
    public Division? Division { get; set; }
}