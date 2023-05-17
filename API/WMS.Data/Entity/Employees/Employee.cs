using WMS.Data.Constant.Enum;
using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Divisions;
using WMS.Data.Entity.Persons;
using WMS.Data.Entity.Positions;
using WMS.Data.Entity.VendorCustomers;

namespace WMS.Data.Entity.Employees;

public class Employee : BaseCatalog
{
    public Division? Division { get; set; }
    public Guid? DivisionId { get; set; }
    public Position? Position { get; set; }
    public Guid? PositionId { get; set; }
    public Person? Person { get; set; }
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
    public VendorCustomer? VendorCustomer { get; set; }
}