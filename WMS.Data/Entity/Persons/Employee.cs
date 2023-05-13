using ERP.Core.Entities.Catalogs.CompanyCatalog;
using ERP.Core.Enums;
using WMS.Data.Entity.Orders;

namespace WMS.Data.Entity.Persons;

public class Employee : BaseCatalog
{
    public Employee()
    {
        ReferenceType = ReferenceType.CatalogEmployee;
    }

    public int OrisId { get; set; }
    public decimal CompanyInsurance { get; set; }
    public decimal CompanySalary { get; set; }
    public Division? Division { get; set; }
    public Guid? DivisionId { get; set; }
    public Position? Position { get; set; }
    public Guid? PositionId { get; set; }
    public Person? Person { get; set; }
    public Guid? PersonId { get; set; }
    public Company? Company { get; set; }
    public Guid? CompanyId { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string? Tin { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public Guid? VendorCustomerId { get; set; }
    public VendorCustomer? VendorCustomer { get; set; }
}