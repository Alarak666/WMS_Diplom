namespace WMS.Core.Services.Framework;

public interface IAppFramework
{
    IAppFramework AddEntity(Type entityType, Action<AppFrameworkEntity> options = default!);
    AppFrameworkEntity GetEntity(Type entityType);
}