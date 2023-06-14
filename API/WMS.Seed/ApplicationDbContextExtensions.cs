
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using WMS.Data.Constant;
using WMS.Data.Constant.Enum;
using WMS.Data.Context;
using WMS.Data.Entity.Identity;

namespace ERP.Seed;

public static class ApplicationDbContextExtensions
{
    private static Faker _faker;
    private static Guid _divisionId;
    private static Guid[] _productIds;
    public static string ConnectionString { get; }

    static ApplicationDbContextExtensions()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        var configuration = builder.Build();

        ConnectionString = configuration.GetConnectionString("DefaultConnection");
    }



    public static async Task EnsureSeedData(this ApplicationDbContext context)
    {
        if (context.Database.GetPendingMigrations().Any()) context.Database.Migrate();

        var stopWatch = new Stopwatch();
        stopWatch.Start();

        await SeedData(context);
        stopWatch.Stop();
        Console.WriteLine($"SeedData elapsed milliseconds {stopWatch.ElapsedMilliseconds}");

    }

    private static async Task SeedApplicationRoles(ApplicationDbContext context)
    {
        var applicationRole = new List<ApplicationRole>()
        {
            new ApplicationRole()
            {
                Name = "Tester",
                Id = CoreDefaultValues.ApplicationRoleId,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                NormalizedName = "TesterPRO"
            },

        };
        await context.AddRangeAsync(applicationRole);
        await context.SaveChangesAsync();
    }

    private static async Task SeedApplicationUserSettingss(ApplicationDbContext context)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        var applicationUserSettings = new List<ApplicationUserSetting>()
        {
            new ApplicationUserSetting()
            {
                Id = CoreDefaultValues.UserSettingsId1,
                ApplicationUser = new ApplicationUser()
                {
                        Id = CoreDefaultValues.CreatedApplicationUserId,
                        Email = _faker.Person.Email,
                        PhoneNumber = _faker.Person.Phone,
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        AccessFailedCount = 0,
                        NormalizedEmail = _faker.Person.Email,
                        NormalizedUserName = "Admin",
                        UserName = "Admin",
                        TwoFactorEnabled = false,
                        ConcurrencyStamp = CoreDefaultValues.CreatedApplicationUserId.ToString(),
                        SecurityStamp = CoreDefaultValues.CreatedApplicationUserId.ToString(),
                        PasswordHash = hasher.HashPassword(new ApplicationUser(), "0")
                },
                CurrentLocale = "Kiev",
                ApplicationUserId = CoreDefaultValues.CreatedApplicationUserId,
                Timezone = DateTime.Now.ToString(),
                VerificationType = VerificationType.Email
            },
        };
        await context.AddRangeAsync(applicationUserSettings);
        await context.SaveChangesAsync();
    }

    private static async Task SeedData(ApplicationDbContext context)
    {

        _faker = new Faker();

        await SeedApplicationRoles(context);
        await SeedApplicationUserSettingss(context);
    }
}