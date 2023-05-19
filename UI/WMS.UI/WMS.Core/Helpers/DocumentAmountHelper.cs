using WMS.Core.Enums;
using WMS.Core.Models.BaseModels;

namespace WMS.Core.Helpers;

public class DocumentAmountHelper
{
    public static void RecalculateVatAmounts(IDocumentTotals documentTotals, IEnumerable<IDocumentAmountLine> lines)
    {
        foreach (var line in lines)
        {
            HandelChangeVat(documentTotals, lines, line, line.VatType);
        }
        
        CalculateTotals(documentTotals, lines);
    }
    
    public static void HandlePriceChanged(IDocumentTotals documentTotals, IEnumerable<IDocumentAmountLine> lines, IDocumentAmountLine line, decimal newPrice)
    {
        line.Price = newPrice;
        line.Amount = line.Quantity * line.Price;
        
        HandelChangeVat(documentTotals, lines, line, line.VatType);
        CalculateTotals(documentTotals, lines);
    }

    public static void HandleQuantityChanged(IDocumentTotals documentTotals, IEnumerable<IDocumentAmountLine> lines, IDocumentAmountLine line, decimal newQuantity)
    {
        line.Quantity = newQuantity;
        line.Amount = line.Quantity * line.Price;

        HandelChangeVat(documentTotals, lines, line, line.VatType);
    }

    public static void HandleVatRateChanged(IDocumentTotals documentTotals, IEnumerable<IDocumentAmountLine> lines, IDocumentAmountLine line, decimal newVatRate)
    {
        line.VatAmount = line.Amount * line.VatRate / 100;
        line.VatRate = newVatRate;
        line.AmountWithVat = line.Quantity * line.Price;
    }

    public static void HandelChangeVat(IDocumentTotals documentTotals, IEnumerable<IDocumentAmountLine> lines, IDocumentAmountLine line, VatType newValue)
    {
        line.VatType = newValue;
        line.VatRate = VatHelper.GetVatRate(newValue);
        if (documentTotals.PriceIncludesVat)
        {
            var amount = VatHelper.CalculateNet(line.Amount, line.VatRate);
            line.VatAmount = line.Amount - amount;
            line.AmountWithVat = line.Amount;
        }
        else
        {
            var amount = VatHelper.CalculateGross(line.Amount, line.VatRate);
            line.VatAmount = amount - line.Amount;
            line.AmountWithVat = amount;
        }
        CalculateTotals(documentTotals, lines);
    }
    
    public static void CalculateTotals(IDocumentTotals documentTotals, IEnumerable<IDocumentAmountLine> lines)
    {
        documentTotals.TotalAmount = lines.Sum(x => x.Amount);
        documentTotals.TotalVatAmount = lines.Sum(x => x.VatAmount);
        documentTotals.TotalAmountWithVat = lines.Sum(x => x.AmountWithVat);
    }
}