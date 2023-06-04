namespace WMS.Core.Models.DocumentModels.IdentityModels;

public class ApplicationRoleDetailViewModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? NormalizedName { get; set; }
    public string? ConcurrencyStamp { get; set; }
}