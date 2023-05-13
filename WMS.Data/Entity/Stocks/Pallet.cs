using WMS.Data.Entity.BaseClass;

namespace WMS.Data.Entity.Stocks
{
    public class Pallet : BaseCatalog
    {
        public AreaType? AreaType { get; set; }
        public Guid? AreaTypeId { get; set; }
        public PalletType Type { get; set; }
        public int Quantity { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
        public int Weight { get; set; }
    }
}