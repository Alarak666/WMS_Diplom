using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Data.Entity.BaseClass;

namespace WMS.Data.Entity.Stocks
{
    public class AreaType : BaseCatalog
    {
        public AreaType? IncludeArea { get; set; }
        public Guid? IncludeAreaId { get; set; }
        public Region? Region { get; set; }
        public Guid? RegionId { get; set; }
        public int RackQty { get; set; }
        public int TermMax { get; set; }
        public int MaxPlace { get; set; }
        public int AvailablePlace { get; set; }
    }
}
