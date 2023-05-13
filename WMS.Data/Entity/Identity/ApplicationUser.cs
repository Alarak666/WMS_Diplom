using ERP.Core.Entities.Activities;
using Microsoft.AspNetCore.Identity;

namespace WMS.Data.Entity.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public Guid? UserSettingsId { get; set; }
    public ApplicationUserSettings? UserSettings { get; set; }
    public virtual ICollection<ApplicationUserActivity> Activities { get; set; }
}