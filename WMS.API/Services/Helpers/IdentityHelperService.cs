using Microsoft.EntityFrameworkCore;
using WMS.Data.Context;

namespace WMS.API.Services.Helpers
{
    public class IdentityHelperService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityHelperService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetUserName()
        {
            return _httpContextAccessor?.HttpContext?.User?.FindFirst("UserName")?.Value;
        }

        public async Task<Guid?> GetUserIdAsync(CancellationToken cancellationToken = default)
        {
            var userName = GetUserName();
            var dbContext = _httpContextAccessor.HttpContext?.RequestServices.GetRequiredService<ApplicationDbContext>();
            var userId =
                (await dbContext!.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken))?.Id;

            return userId;
        }
    }
}

