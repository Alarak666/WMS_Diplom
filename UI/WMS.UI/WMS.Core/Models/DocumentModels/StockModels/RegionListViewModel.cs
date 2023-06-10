namespace WMS.Core.Models.DocumentModels.StockModels
{
    public class RegionListViewModel 
    {
        public Guid Id { get; set; }
        public Guid? CreatedUserId { get; set; }
        public string? CreatedUserName { get; set; }
        public DateTime CreatedDate { get; set; } = default;
        public string? UniqueCode { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
