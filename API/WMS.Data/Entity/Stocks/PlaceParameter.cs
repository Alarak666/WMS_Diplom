using WMS.Data.Entity.BaseClass;

namespace WMS.Data.Entity.Stocks
{
    public class PlaceParameter : BaseCatalog
    {
        public Pallet? Pallet { get; set; }
        public Guid? PalletId { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
    }
}
