using System.Reflection;
using System.Text;
using Blazored.Toast;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WMS.Core.DemoTemplate;
using WMS.Core.Services;
using WMS.Core.Services.BaseServices;
using WMS.Core.Services.UserMessages;
using WMS.UI.Services.Support;
using WMS.UI.Services.UserMessages;
using WMS.UI.Shared;

namespace WMS.UI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddLogging();
        services.AddBlazoredToast();
        services.AddControllers().AddJsonOptions(options =>
        {
            // options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetJsonConverter());
        });
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();

        services.AddHttpClient();

        return services;
    }

    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<IViewEventListener, ViewEventListener>();
        services.AddSingleton<ISalesInfoDataProvider, DataProviderAccessArea>();
        services.AddSingleton<IUserNotificationService, UserNotificationService>();
        services.AddScoped(typeof(IBaseDataService<>), typeof(BaseDataService<>));

        services.AddSingleton<TabPageService>();
        return services;
    }

    public static IdentityBuilder AddIdentityCustom<TUser, TRole>(
        this IServiceCollection services,
        Action<IdentityOptions> setupAction)
        where TUser : class
        where TRole : class
    {
        // Hosting doesn't add IHttpContextAccessor by default
        services.AddHttpContextAccessor();
        // Identity services
        services.TryAddScoped<IUserValidator<TUser>, UserValidator<TUser>>();
        services.TryAddScoped<IPasswordValidator<TUser>, PasswordValidator<TUser>>();
        services.TryAddScoped<IPasswordHasher<TUser>, PasswordHasher<TUser>>();
        services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
        services.TryAddScoped<IRoleValidator<TRole>, RoleValidator<TRole>>();
        // No interface for the error describer so we can add errors without rev'ing the interface
        services.TryAddScoped<IdentityErrorDescriber>();
        services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<TUser>>();
        services.TryAddScoped<ITwoFactorSecurityStampValidator, TwoFactorSecurityStampValidator<TUser>>();
        services.TryAddScoped<IUserClaimsPrincipalFactory<TUser>, UserClaimsPrincipalFactory<TUser, TRole>>();
        services.TryAddScoped<IUserConfirmation<TUser>, DefaultUserConfirmation<TUser>>();
        services.TryAddScoped<UserManager<TUser>>();
        services.TryAddScoped<SignInManager<TUser>>();
        services.TryAddScoped<RoleManager<TRole>>();

        if (setupAction != null)
        {
            services.Configure(setupAction);
        }

        return new IdentityBuilder(typeof(TUser), typeof(TRole), services);
    }
}