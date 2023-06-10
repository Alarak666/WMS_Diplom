using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper.Internal;
using WMS.API.Services.AcceptanceOfGoodServices;
using WMS.API.Services.ApplicationRoleServices;
using WMS.API.Services.ApplicationUserServices;
using WMS.API.Services.ApplicationUserSettingServices;
using WMS.API.Services.AreaTypeServices;
using WMS.API.Services.CountryServices;
using WMS.API.Services.CurrencyServices;
using WMS.API.Services.DivisionServices;
using WMS.API.Services.EmployeeServices;
using WMS.API.Services.Helpers;
using WMS.API.Services.Notifications;
using WMS.API.Services.OrderControllers;
using WMS.API.Services.OrderDetailControllers;
using WMS.API.Services.PalletControllers;
using WMS.API.Services.PersonControllers;
using WMS.API.Services.PlaceParameterControllers;
using WMS.API.Services.PositionControllers;
using WMS.API.Services.ProductControllers;
using WMS.API.Services.RegionControllers;
using WMS.API.Services.UnitControllers;
using WMS.API.Services.UserActivityControllers;
using WMS.Data.Context;
using WMS.Data.DTO.CountryDtos;
using WMS.Data.DTO.CurrencyDtos;
using WMS.Data.DTO.DivisionDtos;
using WMS.Data.DTO.EmployeeDtos;
using WMS.Data.DTO.IdentityDtos;
using WMS.Data.DTO.OrderDtos;
using WMS.Data.DTO.PersonDtos;
using WMS.Data.DTO.PositionDtos;
using WMS.Data.DTO.ProductDtos;
using WMS.Data.DTO.StockDtos;
using WMS.Data.DTO.UnitDtos;
using WMS.Data.DTO.VendorCustomerDtos;
using WMS.Data.Entity.Identity;
using WMS.Data.Entity.Stocks;
using WMS.Data.Interface;
using WMS.Data.Interface.ControllerInterface;
using WMS.Data.MapperProfiles;
using WMS.API.Services.VendorCustomerControllers;

namespace WMS.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<IDocumentRepository<AcceptanceOfGoodDto>, AcceptanceOfGoodService>();
        services.AddScoped<IDocumentRepository<ApplicationRoleDto>, ApplicationRoleService>();
        services.AddScoped<IDocumentRepository<ApplicationUserDto>, ApplicationUserService>();
        services.AddScoped<IDocumentRepository<ApplicationUserSettingDto>, ApplicationUserSettingService>();
        services.AddScoped<IDocumentRepository<AreaTypeDto>, AreaTypeService>();
        services.AddScoped<IDocumentRepository<CountryDto>, CountryService>();
        services.AddScoped<IDocumentRepository<CurrencyDto>, CurrencyService>();
        services.AddScoped<IDocumentRepository<DivisionDto>, DivisionService>();
        services.AddScoped<IDocumentRepository<EmployeeDto>, EmployeeService>();
        services.AddScoped<IDocumentRepository<OrderDto>, OrderService>();
        services.AddScoped<IDocumentRepository<PalletDto>, PalletService>();
        services.AddScoped<IDocumentRepository<VendorCustomerDto>, VendorCustomerService>();
        services.AddScoped<IDocumentRepository<PersonDto>, PersonService>();
        services.AddScoped<IDocumentRepository<PlaceParameterDto>, PlaceParameterService>();
        services.AddScoped<IDocumentRepository<PositionDto>, PositionService>();
        services.AddScoped<IDocumentRepository<ProductDto>, ProductService>();
        services.AddScoped<IDocumentRepository<RegionDto>, RegionService>();
        services.AddScoped<IDocumentRepository<UnitDto>, UnitService>();
        services.AddScoped<IUserActivityService, UserActivityService>();
        services.AddScoped<IDocumentRepository<UnitDto>, UnitService>();




        services.AddScoped(typeof(IDocumentNumeratorService<>), typeof(DocumentNumeratorService<>));
        services.AddScoped<IdentityHelperService>();
        services.AddScoped<TokenService>();
        services.AddScoped<IUserNotificationService, UserNotificationService>();


        services.AddScoped<IDocumentRepository<CurrencyDto>, CurrencyService>();



        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;

                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"])),
                ClockSkew = TimeSpan.Zero
            });

        services.AddScoped<IUserNotificationService, UserNotificationService>();
        services.AddScoped<IdentityHelperService>();
        services.AddScoped<TokenService>();

        services.AddHttpContextAccessor();

        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddAutoMapper(typeof(PersonProfile));
        
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins(configuration.GetSection("CORS:Origins").Get<string[]>());
                builder.AllowCredentials();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
        });

        services.AddDbContextFactory<ApplicationDbContext>(options => { }, ServiceLifetime.Scoped);
        services.AddScoped<IUserActivityService, UserActivityService>();

        return services;
    }
}