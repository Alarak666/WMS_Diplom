namespace WMS.Data.Entity.Persons;
public class Post
{
    public Guid Id { get; set; }
    public string PostName { get; set; } = string.Empty;
    public string Responsibility { get; set; } = string.Empty;
    public decimal Salary { get; set; }
}
