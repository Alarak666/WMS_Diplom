namespace WMS.Core.Services.Framework;

public class AppListView : IAppListView
{
    public string Title { get; set; }
    public Type Model { get; set; }
    public Type ViewComponent { get; set; }
}