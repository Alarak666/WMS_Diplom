using WMS.Core.Services.Framework;

namespace WMS.UI.Extensions;

public static class AppFrameworkServiceCollectionExtensions
{
    public static IServiceCollection AddFrameworkUI(this IServiceCollection services)
    {
        var framework = new AppFramework();

        //framework
        //    .AddEntity(typeof(WorkSchedule), options =>
        //    {
        //        options.AddListView("Work Schedules", typeof(WorkScheduleListViewModel),
        //        typeof(WorkScheduleListViewForm));
        //    });

        // .AddEntity(typeof(SalesOrder));
        services.AddSingleton<IAppFramework>(framework);

        return services;
    }
}
