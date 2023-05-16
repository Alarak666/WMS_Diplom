using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using WMS.API.Extensions;
using WMS.API.Middlewares;
using Swashbuckle.AspNetCore.SwaggerGen;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().WriteTo.File(
        builder.Configuration["LogPath"],
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Information
    ));
    AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.EnableLegacyDatetimeParsing", true);

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddDataServices();
    builder.Services.AddApplicationServices(builder.Configuration);
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSignalR();
    builder.Services.AddHttpClient();
    builder.Services.AddSwaggerGen(c =>
    {
        // Other configuration options...
        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    });
    //builder.Services.AddApiVersioning(options =>
    //{
    //    options.ReportApiVersions = true;
    //    options.AssumeDefaultVersionWhenUnspecified = true;
    //    options.DefaultApiVersion = new ApiVersion(1, 0);
    //    options.ApiVersionReader = ApiVersionReader.Combine(
    //        new QueryStringApiVersionReader("api-version"),
    //        new HeaderApiVersionReader("X-Version"),
    //        new MediaTypeApiVersionReader("ver")
    //    );
    //});

    //builder.Services.AddVersionedApiExplorer(options =>
    //{
    //    options.GroupNameFormat = "'v'VVV";
    //    options.SubstituteApiVersionInUrl = true;
    //});

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
 
    app.Use(async (context, next) =>
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    });
    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    app.MapControllers();
    Log.Information("Application is starting");
    app.Run();
}
catch (SwaggerGeneratorException ex)
{
    Console.WriteLine(ex.InnerException?.ToString());
}
catch (Exception ex)
{
    Debug.WriteLine(ex.Message);
    Console.WriteLine(ex.Message);
    Log.Fatal(ex, "Host terminated unexpectedly");
    Console.WriteLine(ex.Message);
}

finally
{
    Log.CloseAndFlush();
}