using WMS.UI.Model.BaseClassModels;

namespace WMS.UI.Model.StockModels
{
    public class PlaceParameterModel : BaseCatalogModel
    {
        public Guid? PalletId { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
    }
}
