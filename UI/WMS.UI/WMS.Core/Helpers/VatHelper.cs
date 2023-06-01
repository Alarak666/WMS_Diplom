using WMS.Core.Constants.Enum;

namespace WMS.Core.Helpers;

public class VatHelper
{
    public static decimal GetVatRate(VatType vatType)
    {
        switch (vatType)
        {
            case VatType.VatNo:
                return 0;
            case VatType.Vat0:
                return 0;
            case VatType.Vat18:
                return 18;
            default:
                throw new ArgumentOutOfRangeException(nameof(vatType), vatType, null);
        }
    }
    
    public static decimal CalculateVat(decimal amount, decimal vatRate)
    {
        return amount * vatRate / 100;
    }
    
    public static decimal CalculateNet(decimal amount, decimal vatRate)
    {
        return amount / (1 + vatRate / 100);
    }
    
    public static decimal CalculateGross(decimal amount, decimal vatRate)
    {
        return amount * (1 + vatRate / 100);
    }
    
    public static decimal CalculateVatFromGross(decimal amount, decimal vatRate)
    {
        return amount - CalculateNet(amount, vatRate);
    }
    
    public static decimal CalculateNetFromGross(decimal amount, decimal vatRate)
    {
        return amount / (1 + vatRate / 100);
    }
    
    public static decimal CalculateGrossFromNet(decimal amount, decimal vatRate)
    {
        return amount * (1 + vatRate / 100);
    }
    
    public static decimal CalculateVatFromNet(decimal amount, decimal vatRate)
    {
        return amount * vatRate / 100;
    }
    
    public static decimal CalculateNetFromVat(decimal amount, decimal vatRate)
    {
        return amount / vatRate * 100;
    }
    
    public static decimal CalculateGrossFromVat(decimal amount, decimal vatRate)
    {
        return amount / vatRate * 100 * (1 + vatRate / 100);
    }
}