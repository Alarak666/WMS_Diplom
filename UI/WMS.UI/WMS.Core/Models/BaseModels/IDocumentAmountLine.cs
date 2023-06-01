using WMS.Core.Constants.Enum;

namespace WMS.Core.Models.BaseModels;

public interface IDocumentAmountLine
{
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
    public decimal VatAmount { get; set; }
    public decimal AmountWithVat { get; set; }
    public decimal VatRate { get; set; }
    public VatType VatType { get; set; }
}