namespace WMS.Core.Models.DocumentModels.Units
{
    public class UnitListViewModel
    {
        public Guid Id { get; set; }
        public string? CreatedUserName { get; set; }
        public Guid? CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Name { get; set; }
        public string? UniqueCode { get; set; }
    }
}
