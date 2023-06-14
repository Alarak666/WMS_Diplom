using Microsoft.AspNetCore.Components.Server;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
using WMS.API.Middlewares;
using WMS.UI.Data;
using WMS.UI.Extensions;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().WriteTo.File(
        path: builder.Configuration["LogPath"],
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Information
    ));
    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddSingleton<WeatherForecastService>();
    builder.Services.AddFrameworkUI();
    builder.Services.AddApplicationServices();
    builder.Services.AddDataServices();
    builder.Services.AddDevExpressBlazor(
        configure => configure.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5);
    builder.Services.AddRazorPages();
    builder.Services.AddDevExpressBlazor();
    builder.Services.AddDevExpressBlazorWasmMasks();
  
    //builder.Services.AddDevExpressServerSideBlazorReportViewer();
    builder.Services.Configure<DevExpress.Blazor.Configuration.GlobalOptions>(options =>
    {
        options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
    });
    builder.WebHost.UseWebRoot("wwwroot");
    builder.WebHost.UseStaticWebAssets();

    var app = builder.Build();
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.Use(async (context, next) =>
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    });
    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    app.UseStaticFiles();
    app.UseDevExpressBlazorWasmMasksStaticFiles();
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseStaticFiles();

    app.UseRouting();

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    Log.Information("Application is starting");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
    Debug.WriteLine(ex.Message);
}
finally
{
    Log.CloseAndFlush();
}