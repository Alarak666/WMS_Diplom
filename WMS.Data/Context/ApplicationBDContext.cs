using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Countries;
using WMS.Data.Entity.Currencies;
using WMS.Data.Entity.Divisions;
using WMS.Data.Entity.Employees;
using WMS.Data.Entity.Identity;
using WMS.Data.Entity.Notifications;
using WMS.Data.Entity.Orders;
using WMS.Data.Entity.Persons;
using WMS.Data.Entity.Positions;
using WMS.Data.Entity.Products;
using WMS.Data.Entity.Stocks;
using WMS.Data.Entity.Units;
using WMS.Data.Entity.VendorCustomers;

namespace WMS.Data.Context
{
    public class MainDbDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("WMS.Data"));
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        static ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.EnableLegacyDatetimeParsing", true);
        }
        public DbSet<BaseCatalog> BaseCatalogs { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationUserSetting> ApplicationUserSettings { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AcceptanceOfGood> AcceptanceOfGoods { get; set; }
        public DbSet<AreaType> AreaTypes { get; set; }
        public DbSet<Pallet> Pallets { get; set; }
        public DbSet<PlaceParameter> PlaceParameters { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<VendorCustomer> VendorCustomers { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.UserSettings)
                .WithOne(us => us.ApplicationUser)
                .HasForeignKey<ApplicationUserSetting>(us => us.ApplicationUserId);
            modelBuilder.Entity<BaseCatalog>().ToTable("BaseCatalogs");
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Currency>().ToTable("Currencies");
            modelBuilder.Entity<Division>().ToTable("Divisions");
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<ApplicationRole>().ToTable("ApplicationRoles");
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
            modelBuilder.Entity<ApplicationUserSetting>().ToTable("ApplicationUserSettings");
            modelBuilder.Entity<UserActivity>().ToTable("UserActivities");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails");
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Position>().ToTable("Positions");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<AcceptanceOfGood>().ToTable("AcceptanceOfGoods");
            modelBuilder.Entity<AreaType>().ToTable("AreaTypes");
            modelBuilder.Entity<Pallet>().ToTable("Pallets");
            modelBuilder.Entity<PlaceParameter>().ToTable("PlaceParameters");
            modelBuilder.Entity<Region>().ToTable("Regions");
            modelBuilder.Entity<Unit>().ToTable("Units");
            modelBuilder.Entity<VendorCustomer>().ToTable("VendorCustomers");
            modelBuilder.Entity<VendorCustomer>().ToTable("UserMessages");
            
            modelBuilder.Entity<Position>()
                .Property(p => p.MainSalary)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(p => p.Quantity)
                .HasPrecision(18, 2);
            modelBuilder.Entity<OrderDetail>()
                .Property(p => p.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Product>()
                .Property(p => p.ImportPrice)
                .HasPrecision(18, 2);
        }
    }

}

    

    


    

    

    

    

    

    


