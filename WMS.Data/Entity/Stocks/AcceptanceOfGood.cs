using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Employees;
using WMS.Data.Entity.Products;

namespace WMS.Data.Entity.Stocks
{
    public class AcceptanceOfGood : BaseCatalog
    {
        public Guid? TypePalletId { get; set; }
        public Pallet? TypePallet { get; set; }
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid? EmployerId { get; set; }
        public Employee? Employer { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public int Qty { get; set; }
        public DateTime DateAccepts { get; set; }
        public DateTime DataExpiration { get; set; }
        public string NPallet { get; set; } = string.Empty;
    }
}
