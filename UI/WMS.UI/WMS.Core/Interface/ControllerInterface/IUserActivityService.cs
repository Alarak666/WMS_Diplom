namespace WMS.Core.Interface.ControllerInterface;

public interface IUserActivityService
{
    Task AddCurrentUserActivity(string activityDescription);
}