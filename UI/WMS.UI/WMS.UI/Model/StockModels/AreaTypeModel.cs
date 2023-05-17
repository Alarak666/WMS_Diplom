using WMS.UI.Model.BaseClassModels;

namespace WMS.UI.Model.StockModels
{
    public class AreaTypeModel : BaseCatalogModel
    {
        public Guid? IncludeAreaId { get; set; }
        public Guid? RegionId { get; set; }
        public int RackQty { get; set; }
        public int TermMax { get; set; }
        public int MaxPlace { get; set; }
        public int AvailablePlace { get; set; }
    }
}
