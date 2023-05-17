using WMS.UI.Constant.Enum;

namespace WMS.UI.Model.IdentityModels;

public class ApplicationUserSettingModel
{
    public Guid Id { get; set; }
    public Guid? ApplicationUserId { get; set; }
    public string? CurrentLocale { get; set; }
    public string? Timezone { get; set; }
    public VerificationType VerificationType { get; set; }
    public Guid? PositionId { get; set; }
}