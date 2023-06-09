namespace WMS.Core.Models.DocumentModels.StockModels
{
    public class PalletListViewModel 
    {
        public Guid Id { get; set; }
        public Guid? CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; } = default;
        public string? UniqueCode { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? AreaTypeId { get; set; }
        public PalletType Type { get; set; }
        public int Quantity { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }
        public decimal Weight { get; set; }
    }
}