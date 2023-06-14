namespace WMS.API.Services.ApplicationUserServices;

public interface IApplicationUserService
{
    Task<bool> Login(string name, string password, CancellationToken cancellation);
}