using WMS.Core.DTO.BaseClassDtos;

namespace WMS.Core.DTO.StockDtos
{
    public class PalletDto : BaseCatalogDto
    {
        public Guid? AreaTypeId { get; set; }
        public PalletType Type { get; set; }
        public int Quantity { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
        public int Weight { get; set; }
    }
}