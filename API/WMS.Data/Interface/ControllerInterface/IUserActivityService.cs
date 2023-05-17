namespace WMS.Data.Interface.ControllerInterface;

public interface IUserActivityService
{
    Task AddCurrentUserActivity(string activityDescription);
}