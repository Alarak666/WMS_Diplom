
namespace WMS.Core.Services.Activities;

public interface IApplicationUserActivityService
{
    //Task<IEnumerable<ApplicationUserActivity>> GetAll();
    //Task<ApplicationUserActivity> GetRecordById(Guid id);
    //Task<ApplicationUserActivity> AddRecord(ApplicationUserActivity newApplicationUserActivity);
    //Task UpdateRecord(ApplicationUserActivity applicationUserActivity);
    Task DeleteRecord(Guid id);
}