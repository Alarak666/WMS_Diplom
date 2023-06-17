using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using WMS.Data.Constant;
using WMS.Data.Constant.Enum;
using WMS.Data.Context;
using WMS.Data.Entity.Divisions;
using WMS.Data.Entity.Identity;
using WMS.Data.Entity.Positions;
using WMS.Data.Entity.Products;
using WMS.Data.Entity.Stocks;
using WMS.Data.Entity.Units;
using WMS.Data.Entity.VendorCustomers;

namespace WMS.Seed;

public static class ApplicationDbContextExtensions
{
    private static Faker _faker;
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
    private static async Task SeedUnit(ApplicationDbContext context)
    {
        var applicationRole = new List<Unit>()
        {
            new Unit
            {
                Id = CoreDefaultValues.UnitSH,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "00001",
                Name = "Шт",
            },
            new Unit
            {
                Id = CoreDefaultValues.UnitYp,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "00002",
                Name = "Упак",
            },

        };
        await context.AddRangeAsync(applicationRole);
        await context.SaveChangesAsync();
    }
    private static async Task SeedVendorCustomer(ApplicationDbContext context)
    {
        var applicationRole = new List<VendorCustomer>()
        {
            new VendorCustomer
            {
                Id = CoreDefaultValues.VendorCustomerId,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "00002",
                Name = "Anatolio",
                Tin = "100101010",
                LegalAddress = _faker.Person.Address.State,
                ActualAddress = _faker.Person.Address.State,
                PhoneNumber = _faker.Person.Phone,
                Description = _faker.Person.Company.Bs,
                Email =  _faker.Person.Email,
                IsEmailValidVendorCustomer = true,
                Other = "",
                Additional = "",
                IsCustomer = true,
                IsVendor = false,
                IsOther = false,
                IsForeigner = false,
            },
        };
        await context.AddRangeAsync(applicationRole);
        await context.SaveChangesAsync();
    }
    private static async Task SeedRegion(ApplicationDbContext context)
    {
        var applicationRole = new List<Region>()
        {
            new Region
            {
                Id = CoreDefaultValues.RegionId,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "0000001",
                Name = "Region1",
                Description = ""
            },
            new Region
            {
                Id = CoreDefaultValues.RegionId1,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "0000002",
                Name = "Region2",
                Description = ""
            },
            new Region
            {
                Id = CoreDefaultValues.RegionId2,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "0000003",
                Name = "Region3",
                Description = ""
            },
            new Region
            {
                Id = CoreDefaultValues.RegionId3,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "0000004",
                Name = "Region4",
                Description = ""
            }
        };
        await context.AddRangeAsync(applicationRole);
        await context.SaveChangesAsync();
    }
    private static async Task SeedArea(ApplicationDbContext context)
    {
        var applicationRole = new List<AreaType>()
        {
            new AreaType
            {
                Id = CoreDefaultValues.AreaId,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "0000001",
                Name = "Area1",
                RegionId = CoreDefaultValues.RegionId,
                RackQty = 10,
                TermMax = 30,
                MaxPlace = 100,
                AvailablePlace = 10 * 100,

            },
            new AreaType
            {
                Id = CoreDefaultValues.AreaId1,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "0000002",
                Name = "Area2",
                RegionId = CoreDefaultValues.RegionId1,
                RackQty = 10,
                TermMax = 30,
                MaxPlace = 100,
                AvailablePlace = 10 * 100,
            },
            new AreaType
            {
                Id = CoreDefaultValues.AreaId2,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "0000003",
                Name = "Area3",
                RegionId = CoreDefaultValues.RegionId2,
                RackQty = 10,
                TermMax = 30,
                MaxPlace = 100,
                AvailablePlace = 10 * 100,
            },
            new AreaType
            {
                Id = CoreDefaultValues.AreaId3,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "0000004",
                Name = "Area4",
                RegionId = CoreDefaultValues.RegionId3,
                RackQty = 10,
                TermMax = 30,
                MaxPlace = 100,
                AvailablePlace = 10 * 100,
            }
        };
        await context.AddRangeAsync(applicationRole);
        await context.SaveChangesAsync();
    }
    private static async Task SeedPlaceParameter(ApplicationDbContext context)
    {
        var applicationRole = new List<PlaceParameter>()
        {
            new PlaceParameter
            {
                Id = CoreDefaultValues.PlaceParameterId,
                CreatedUser = null,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "0000001",
                Name = "PlaceParameter1",
                PalletId = CoreDefaultValues.Pallet,
                Width = 1300,
                Length = 900,
                Height = 2000,
                Weight = 1200,
            },
            new PlaceParameter
            {
                Id = CoreDefaultValues.PlaceParameterId1,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "0000002",
                Name = "PlaceParameter2",
                PalletId = CoreDefaultValues.Pallet1,
                Width = 1300,
                Length = 900,
                Height = 2000,
                Weight = 1200,
            },
            new PlaceParameter
            {
                Id = CoreDefaultValues.PlaceParameterId2,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "0000003",
                Name = "PlaceParameter3",
                PalletId = CoreDefaultValues.Pallet2,
                Width = 1300,
                Length = 900,
                Height = 2000,
                Weight = 1200,
            },
        };
        await context.AddRangeAsync(applicationRole);
        await context.SaveChangesAsync();

    }
    private static async Task SeedPallet(ApplicationDbContext context)
    {
        var applicationRole = new List<Pallet>()
        {
            new Pallet
            {
                Id = CoreDefaultValues.Pallet,
                CreatedUser = null,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "0000001",
                Name = @"Standard",
                Type = PalletType.Standard,
                Quantity = 150,
                Width = 1000,
                Height = 145,
                Length = 1200,
                Weight = 600,
            },
            new Pallet
            {
                Id = CoreDefaultValues.Pallet1,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "0000002",
                Name = "Industrial",
                Type = PalletType.Industrial,
                Quantity = 150,
                Width = 800,
                Height = 145,
                Length = 1200,
                Weight = 800,
            },
            new Pallet
            {
                Id = CoreDefaultValues.Pallet2,
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = "0000003",
                Name = "EUR",
                AreaType = null,
                AreaTypeId = null,
                Type = PalletType.Euro,
                Quantity = 0,
                Width = 800,
                Height = 145,
                Length = 1200,
                Weight = 1000,
            },
        };
        await context.AddRangeAsync(applicationRole);
        await context.SaveChangesAsync();
    }
    private static async Task SeedPosition(ApplicationDbContext context)
    {
        var positions = new List<Position>();
        var faker = new Faker();
        var post = new string[]
        {
            "Staff",
            "Accountant",
            "Manager"
        };
        var div = new string[]
        {
            "Stock",
            "Sales",
            "HR",
        };
        for (int i = 0; i < 3; i++)
        {
            positions.Add(new Position
            {
                Id = Guid.NewGuid(),
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = $"000000{i + 1}",
                Name = post[i],
                Description = faker.Lorem.Sentence(),
                DateOfApproval = faker.Date.Past(),
                PositionApproved = faker.Random.Bool(),
                MainSalary = faker.Random.Decimal(1000, 5000),
                DivisionId = context.BaseCatalogs.FirstOrDefaultAsync(x => x.Name == div[i])?.Result?.Id,
            });
        }

        await context.AddRangeAsync(positions);
        await context.SaveChangesAsync();
    }
    private static async Task SeedProduct(ApplicationDbContext context)
    {
        var products = new List<Product>() { };
        for (int i = 0; i < 100; i++)
        {
            _faker = new Faker();
            products.Add(new Product
            {
                Id = Guid.NewGuid(),
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = $"000000{i + 1}",
                Name = _faker.Commerce.ProductName(),
                ItemType = ItemType.Goods,
                Description = _faker.Commerce.ProductDescription(),
                Barcode = _faker.Commerce.Ean13(),
                VendorCode = _faker.Random.AlphaNumeric(8).ToUpper(),
                MainUnitId = CoreDefaultValues.UnitYp,
                VatRate = VatType.VatNo,
                UnitId = CoreDefaultValues.UnitYp,
                Import = false,
                ImportPrice = null,
                Price = _faker.Random.Decimal(1, 1000)
            });
        }

        await context.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
    private static async Task SeedDivision(ApplicationDbContext context)
    {
        var division = new List<Division>() { };
        var div = new string[]
        {
            "Stock",
            "Sales",
            "HR",
        };
        for (int i = 0; i < 3; i++)
        {

            _faker = new Faker();
            division.Add(new Division
            {
                Id = Guid.NewGuid(),
                CreatedUserId = CoreDefaultValues.CreatedApplicationUserId,
                CreatedDate = DateTime.Now,
                UniqueCode = $"000000{i + 1}",
                Name = div[i].ToString(),
            });
        }
        await context.AddRangeAsync(division);
        await context.SaveChangesAsync();
    }
    private static async Task SeedData(ApplicationDbContext context)
    {
        _faker = new Faker();
        await SeedApplicationRoles(context);
        await SeedApplicationUserSettingss(context);

        await SeedDivision(context);
        await SeedPosition(context);


        await SeedRegion(context);
        await SeedArea(context);

        await SeedPallet(context);
        await SeedPlaceParameter(context);
        await SeedUnit(context);
        await SeedProduct(context);


        await SeedVendorCustomer(context);
    }


}