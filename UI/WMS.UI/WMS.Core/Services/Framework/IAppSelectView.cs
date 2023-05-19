namespace WMS.Core.Services.Framework;

public interface IAppSelectView
{
    public string Title { get; set; }
    public Type Model { get; set; }
    public Type ViewComponent { get; set; }
}