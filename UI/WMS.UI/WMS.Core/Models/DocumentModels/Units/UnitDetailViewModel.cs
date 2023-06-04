namespace WMS.Core.Models.DocumentModels.Units
{
    public class UnitDetailViewModel
    {
        public Guid? Id { get; set; }
        public string? CreatedUserUserName { get; set; }
        public Guid? CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Name { get; set; }
        public string? UniqueCode { get; set; }
        public int RsUnitId { get; set; }
    }
}
