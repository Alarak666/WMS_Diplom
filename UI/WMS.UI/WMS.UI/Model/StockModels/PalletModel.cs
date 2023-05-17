using WMS.UI.Constant.Enum;
using WMS.UI.Model.BaseClassModels;

namespace WMS.UI.Model.StockModels
{
    public class PalletModel : BaseCatalogModel
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