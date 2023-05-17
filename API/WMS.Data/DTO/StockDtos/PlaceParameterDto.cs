using WMS.Data.DTO.BaseClassDtos;
using WMS.Data.Entity.Stocks;

namespace WMS.Data.DTO.StockDtos
{
    public class PlaceParameterDto : BaseCatalogDto
    {
        public Guid? PalletId { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
    }
}
