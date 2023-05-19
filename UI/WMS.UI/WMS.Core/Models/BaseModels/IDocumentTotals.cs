namespace WMS.Core.Models.BaseModels;

public interface IDocumentTotals
{
    public bool PriceIncludesVat { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalVatAmount { get; set; }
    public decimal TotalAmountWithVat { get; set; }
}