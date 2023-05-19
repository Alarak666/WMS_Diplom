namespace WMS.Core.Services.Framework;

public class AppFramework : IAppFramework
{
    private List<AppFrameworkEntity> Entities { get; set; } = new List<AppFrameworkEntity>();
    
    public IAppFramework AddEntity(Type entityType, Action<AppFrameworkEntity> options = default!)
    {
        var newEntity = new AppFrameworkEntity()
        {
            EntityType = entityType,
        };
        options?.Invoke(newEntity);
        Entities.Add(newEntity);
        return this;
    }
    
    public AppFrameworkEntity GetEntity(Type entityType)
    {
        return Entities.FirstOrDefault(e => e.EntityType == entityType);
    }

}