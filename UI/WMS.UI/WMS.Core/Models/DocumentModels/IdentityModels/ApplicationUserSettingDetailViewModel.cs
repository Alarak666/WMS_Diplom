using WMS.Core.Constants.Enum;

namespace WMS.Core.Models.DocumentModels.IdentityModels;

public class ApplicationUserSettingDetailViewModel
{
    public Guid Id { get; set; }
    public Guid? ApplicationUserId { get; set; }
    public string? ApplicationUserName { get; set; }
    public string? CurrentLocale { get; set; }
    public string? Timezone { get; set; }
    public VerificationType VerificationType { get; set; }
    public Guid? PositionId { get; set; }
}