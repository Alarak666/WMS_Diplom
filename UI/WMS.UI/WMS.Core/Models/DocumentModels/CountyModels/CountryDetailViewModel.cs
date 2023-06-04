namespace WMS.Core.Models.DocumentModels.CountyModels
{
    public class CountryDetailViewModel
    {
        public Guid Id { get; set; }
        public Guid? CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; } = default;
        public string? UniqueCode { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public Guid? CurrencyId { get; set; }
    }
}
