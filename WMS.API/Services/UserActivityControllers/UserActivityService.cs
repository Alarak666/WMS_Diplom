using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WMS.API.Services.Helpers;
using WMS.Data.Context;
using WMS.Data.Entity.Identity;
using WMS.Data.Interface.ControllerInterface;

namespace WMS.API.Services.UserActivityControllers;

public class UserActivityService : IUserActivityService
{
    private readonly ApplicationDbContext _context;
    private readonly IdentityHelperService _identityHelperService;

    public UserActivityService(ApplicationDbContext context, IdentityHelperService identityHelperService)
    {
        _context = context;
        _identityHelperService = identityHelperService;
    }
    public async Task AddCurrentUserActivity(string activityDescription)
    {
        var userName = _identityHelperService.GetUserName();
        if (userName is not null)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName == userName);
            var newApplicationUserActivity = new UserActivity
            {
                ActivityDate = DateTime.Now,
                ApplicationUserId = user.Id,
                ActivityDescription = activityDescription
            };
            _context.UserActivities.Add(newApplicationUserActivity);
            await _context.SaveChangesAsync();
        }
    }
}