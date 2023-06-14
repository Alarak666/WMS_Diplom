// See https://aka.ms/new-console-template for more information


using ERP.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WMS.Data.Context;

Console.WriteLine("Seed data applying...");

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false);

IConfiguration config = builder.Build();

var connectionString = config.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("Connection string is empty!");
    return;
}

Console.WriteLine($"Connection string: {connectionString}");

var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionsBuilder.UseSqlServer(connectionString);

using var dbContext = new ApplicationDbContext(optionsBuilder.Options);

dbContext.Database.EnsureDeleted();
dbContext.Database.Migrate();
dbContext.EnsureSeedData().GetAwaiter().GetResult();

Console.WriteLine("Seed data applied. ");
// Console.ReadKey();