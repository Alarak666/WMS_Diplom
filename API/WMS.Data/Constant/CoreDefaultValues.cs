namespace WMS.Data.Constant
{
    public class CoreDefaultValues
    {
        public const string Version = "0.0.1";
        public static Guid UserSettingsId1 { get; set; } = new Guid("ea5ec24d-f2a8-4481-ba85-383138ba7eac");
        public static Guid CreateUserId { get; set; } = new Guid("abc298b3-3670-4353-9a4c-508dd654dab5");
        public static Guid CreatedApplicationUserId { get; set; } = new Guid("b47ec509-b9cb-4e0a-ba3c-291edda5f1fb");
        public static Guid ApplicationRoleId { get; set; } = new Guid("189aea1e-d2d8-48f0-8bfa-c7c842e50899");
    }
}
