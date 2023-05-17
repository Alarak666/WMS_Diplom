namespace WMS.UI.Model.IdentityModels;

public class UserActivityModel
{
    public Guid Id { get; set; }
    public Guid ApplicationUserId { get; set; }
    public string? ActivityDescription { get; set; }
    public DateTime ActivityDate { get; set; }
}