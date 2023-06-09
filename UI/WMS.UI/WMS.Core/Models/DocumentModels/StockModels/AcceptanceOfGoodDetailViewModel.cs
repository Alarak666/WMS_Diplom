namespace WMS.Core.Models.DocumentModels.StockModels
{
    public class AcceptanceOfGoodDetailViewModel
    {
        public Guid Id { get; set; }
        public Guid? CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; } = default;
        public string? UniqueCode { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? TypePalletId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? EmployerId { get; set; }
        public string EmployerName { get; set; } = string.Empty;
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public int Qty { get; set; }
        public DateTime DateAccepts { get; set; }
        public DateTime DataExpiration { get; set; }
        public string NPallet { get; set; }
    }
}
