using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Core.Models.Dashboards
{
    public class DashBoardPalletDto
    {
        public string PalletName { get; set; } = string.Empty;
        public Guid? AreaTypeId { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public PalletType Type { get; set; }
        public int Quantity { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
        public int Weight { get; set; }
        public int M3 => (Width * Height * Length) / 1000000;

    }
}
