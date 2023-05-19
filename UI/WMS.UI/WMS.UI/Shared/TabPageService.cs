namespace WMS.UI.Shared;

public class TabPageService
{
    public delegate void TabPageChangedEventHandler(object sender);
    public event TabPageChangedEventHandler TabPageChanged;
    
    public List<TabPageDescription> TabComponents = new List<TabPageDescription>();
    public int CurrentTabIndex { get; protected set; } = 0;

    public async Task AddTabPage(TabPageDescription newTab)
    {
        TabComponents.Add(newTab);
        CurrentTabIndex = TabComponents.Count - 1;
        TabPageChanged?.Invoke(this);
    }
    
    public async Task RemoveTabPage(TabPageDescription tab)
    {
        TabComponents.Remove(tab);
        CurrentTabIndex -= 1;
        TabPageChanged?.Invoke(this);
    }


    public void SetCurrentIndex(int newIndex)
    {
        CurrentTabIndex = newIndex;
        TabPageChanged?.Invoke(this);
    }
}