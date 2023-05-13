using Microsoft.AspNetCore.Identity;

namespace WMS.Data.Entity.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public Guid? UserSettingsId { get; set; }
    public ApplicationUserSetting? UserSettings { get; set; }
    public virtual ICollection<UserActivity> Activities { get; set; }
}