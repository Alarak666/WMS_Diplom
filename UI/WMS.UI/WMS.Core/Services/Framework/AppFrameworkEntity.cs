namespace WMS.Core.Services.Framework;

public class AppFrameworkEntity : IAppFrameworkEntity
{
    public Type EntityType { get; set; } = default!;
    public IAppDetailView DetailView { get; set; } = default!;
    public IAppSelectView SelectView { get; set; } = default!;
    public IAppListView ListView { get; set; } = default!;

    public IAppDetailView AddDetailView(string title, Type model, Type viewComponent)
    {
        DetailView = new AppDetailView
        {
            Model = model,
            Title = title,
            ViewComponent = viewComponent
        };
        
        return DetailView;
    }

    public IAppListView AddListView(string title, Type model, Type viewComponent)
    {
        ListView = new AppListView
        {
            Model = model,
            Title = title,
            ViewComponent = viewComponent
        };

        return ListView;
    }

    public IAppSelectView AddSelectView(string title, Type model, Type viewComponent)
    {
        SelectView = new AppSelectView
        {
            Model = model,
            Title = title,
            ViewComponent = viewComponent
        };

        return SelectView;
    }
}