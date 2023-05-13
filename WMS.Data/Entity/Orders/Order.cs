using WMS.Data.Entity.Persons;

namespace WMS.Data.Entity.Orders;
public class Order
{
    public Guid Id { get; set; }
    public Employee? Employee { get; set; }
    public Guid? EmployeeId { get; set; }
    public DateTime Date_Orders { get; set; }
}
