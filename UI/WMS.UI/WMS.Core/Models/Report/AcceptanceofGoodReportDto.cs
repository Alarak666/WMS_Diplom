namespace WMS.Core.Models.Report
{
    public class AcceptanceofGoodReportDto
    {
        public Guid? TypePalletId { get; set; }
        public string TypePalletName { get; set; } = string.Empty;
        public Guid? ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string EmployerName { get; set; } = string.Empty;
        public Guid? EmployerId { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public int Qty { get; set; }
        public DateTime DateAccepts { get; set; }
        public DateTime? DataExpiration { get; set; }
        public string? NPallet { get; set; }
    }
}
