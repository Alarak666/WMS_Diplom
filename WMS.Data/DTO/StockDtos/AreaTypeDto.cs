using WMS.Data.DTO.BaseClassDtos;

namespace WMS.Data.DTO.StockDtos
{
    public class AreaTypeDto : BaseCatalogDto
    {
        public Guid? IncludeAreaId { get; set; }
        public Guid? RegionId { get; set; }
        public int RackQty { get; set; }
        public int TermMax { get; set; }
        public int MaxPlace { get; set; }
        public int AvailablePlace { get; set; }
    }
}
