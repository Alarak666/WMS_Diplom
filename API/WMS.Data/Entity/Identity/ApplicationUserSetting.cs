using WMS.Data.Constant.Enum;

namespace WMS.Data.Entity.Identity;

public class ApplicationUserSetting
{
    public Guid Id { get; set; }
    public Guid? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
    public string? CurrentLocale { get; set; }
    public string? Timezone { get; set; }
    public VerificationType VerificationType { get; set; }
}