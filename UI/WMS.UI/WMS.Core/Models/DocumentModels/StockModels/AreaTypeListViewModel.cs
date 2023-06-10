namespace WMS.Core.Models.DocumentModels.StockModels
{
    public class AreaTypeListViewModel 
    {
        public Guid Id { get; set; }
        public Guid? CreatedUserId { get; set; }
        public string? CreatedUserName { get; set; }
        public DateTime CreatedDate { get; set; } = default;
        public string? UniqueCode { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? IncludeAreaId { get; set; }
        public Guid? RegionId { get; set; }
        public int RackQty { get; set; }
        public int TermMax { get; set; }
        public int MaxPlace { get; set; }
        public int AvailablePlace { get; set; }
    }
}
