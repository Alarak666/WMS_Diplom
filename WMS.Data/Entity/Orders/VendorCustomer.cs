using WMS.Data.Entity.Persons;

namespace WMS.Data.Entity.Orders;

public class VendorCustomer
{
    public string Description { get; set; } = string.Empty;
    public bool VatTaxable { get; set; }
    public string? Tin { get; set; }
    public string? LegalAddress { get; set; }
    public string? ActualAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool IsEmailValidVendorCustomer { get; set; }
    public string? Other { get; set; }
    public string? Additional { get; set; }
    public Country? Country { get; set; }
    public Guid? CountryId { get; set; }
    public bool IsCustomer { get; set; }
    public bool IsVendor { get; set; }
    public bool IsOther { get; set; }
    public bool IsForeigner { get; set; }
}