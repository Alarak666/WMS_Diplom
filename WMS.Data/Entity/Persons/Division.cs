namespace WMS.Data.Entity.Persons;

public class Division
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Division? ParentDivision { get; set; }
    public Guid? ParentDivisionId { get; set; }
}