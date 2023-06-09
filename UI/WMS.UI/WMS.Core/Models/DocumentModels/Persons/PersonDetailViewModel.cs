using WMS.Core.Constants.Enum;

namespace WMS.Core.Models.DocumentModels.Persons
{
    public class PersonDetailViewModel
    {
        public string? Email { get; set; }
        public bool IsEmailValidPerson { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ActualAddress { get; set; }
        public DateTime ValidToDate { get; set; }
        public DateTime IssuedDate { get; set; }
        public string? IssuedBy { get; set; }
        public string? DocumentNumber { get; set; }
        public string? DocumentSeries { get; set; }
        public string? DocumentType { get; set; }
        public string? Citizenship { get; set; }
        public string? AddressOfBirth { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? Tin { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public Guid Id { get; set; }
        public string? CreatedUserUserName { get; set; }
        public Guid? CreatedUserId { get; set; }
        public string? CreatedUserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Name { get; set; }
        public string? UniqueCode { get; set; }
    }
}
