namespace WMS.Core.Services.Framework;

public interface IAppFrameworkEntity
{
    public Type EntityType { get; }
    public IAppDetailView DetailView { get; }
    public IAppSelectView SelectView { get; }
    public IAppListView ListView { get; }
}