using WMS.Data.DTO.BaseClassDtos;
using WMS.Data.Entity.Products;

namespace WMS.Data.DTO.StockDtos
{
    public class AcceptanceOfGoodDto : BaseCatalogDto
    {
        public Guid TypePalletId { get; set; }
        public Guid ProductId { get; set; }
        public Guid EmployerId { get; set; }
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
