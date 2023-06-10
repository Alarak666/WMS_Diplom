using Blazored.Toast;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WMS.Core.DemoTemplate;
using WMS.Core.Interface.ControllerInterface;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Services;
using WMS.Core.Services.BaseServices;
using WMS.UI.Services.DocumentService.AcceptanceOfGoodServices;
using WMS.UI.Services.DocumentService.ApplicationRoleServices;
using WMS.UI.Services.DocumentService.ApplicationUserServices;
using WMS.UI.Services.DocumentService.ApplicationUserSettingServices;
using WMS.UI.Services.DocumentService.AreaTypeServices;
using WMS.UI.Services.DocumentService.CountryServices;
using WMS.UI.Services.DocumentService.CurrencyServices;
using WMS.UI.Services.DocumentService.DivisionServices;
using WMS.UI.Services.DocumentService.EmployeeServices;
using WMS.UI.Services.DocumentService.OrderServices;
using WMS.UI.Services.DocumentService.PalletServices;
using WMS.UI.Services.DocumentService.PersonServices;
using WMS.UI.Services.DocumentService.PlaceParameterServices;
using WMS.UI.Services.DocumentService.PositionServices;
using WMS.UI.Services.DocumentService.ProductServices;
using WMS.UI.Services.DocumentService.RegionServices;
using WMS.UI.Services.DocumentService.UnitServices;
using WMS.UI.Services.DocumentService.VendorCustomerServices;
using WMS.UI.Services.HttpClients;
using WMS.UI.Services.Support;
using WMS.UI.Services.UserMessages;
using WMS.UI.Shared;

namespace WMS.UI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddLogging();
        services.AddControllers().AddJsonOptions(options =>
        {
            // options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetJsonConverter());
        });
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddBlazoredToast();
        services.AddHttpClient();

        return services;
    }

    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.Configure<CircuitOptions>(options =>
        {
            options.DetailedErrors = true;
        });

        #region RepositoryDocument
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<IDivisionService, DivisionService>();
        services.AddScoped<IApplicationRoleService, ApplicationRoleService>();
        services.AddScoped<IApplicationUserService, ApplicationUserService>();
        services.AddScoped<IApplicationUserSettingService, ApplicationUserSettingService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAcceptanceOfGoodService, AcceptanceOfGoodService>();
        services.AddScoped<IAreaTypeService, AreaTypeService>();
        services.AddScoped<IPalletService, PalletService>();
        services.AddScoped<IPlaceParameterService, PlaceParameterService>();
        services.AddScoped<IRegionService, RegionService>();
        services.AddScoped<IUnitService, UnitService>();
        services.AddScoped<IVendorCustomerService, VendorCustomerService>();

        #endregion

        services.AddScoped<IViewEventListener, ViewEventListener>();
        services.AddSingleton<ISalesInfoDataProvider, DataProviderAccessArea>();
        services.AddSingleton<IUserNotificationService, UserNotificationService>();
        services.AddScoped(typeof(IBaseDataService<>), typeof(BaseDataService<>));
        services.AddScoped<HttpClientHelper>();
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