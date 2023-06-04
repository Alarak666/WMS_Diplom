namespace WMS.Core.Models.DocumentModels.VendorCustomers
{
    public class VendorCustomerListViewModel
    {
        public string? ParentName { get; set; }
        public Guid? ParentId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool VatTaxable { get; set; }
        public string? Tin { get; set; }
        public string? LegalForm { get; set; }
        public string? MainBankAccountName { get; set; }
        public Guid? MainBankAccountId { get; set; }
        public string? LegalAddress { get; set; }
        public string? ActualAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsEmailValidVendorCustomer { get; set; }
        public string? Other { get; set; }
        public string? Additional { get; set; }
        public string? CountryName { get; set; }
        public Guid? CountryId { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsVendor { get; set; }
        public bool IsOther { get; set; }
        public Guid Id { get; set; }
        public string? CreatedUserName { get; set; }
        public Guid? CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Name { get; set; }
        public string? UniqueCode { get; set; }
    }
}
