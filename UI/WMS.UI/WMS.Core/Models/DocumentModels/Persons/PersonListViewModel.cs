using WMS.Core.Constants.Enum;

namespace WMS.Core.Models.DocumentModels.Persons
{
    public class PersonListViewModel
    {
        public string? Email { get; set; }
        public bool IsEmailValidPerson { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ActualAddress { get; set; }
        public DateTime ValidToDate { get; set; } = DateTime.UtcNow;
        public DateTime IssuedDate { get; set; } = DateTime.UtcNow;
        public string? IssuedBy { get; set; }
        public string? DocumentNumber { get; set; }
        public string? DocumentSeries { get; set; }
        public string? DocumentType { get; set; }
        public string? Citizenship { get; set; }
        public string? AddressOfBirth { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
        public Gender Gender { get; set; }
        public string? Tin { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public Guid Id { get; set; }
        public Guid? CreatedUserId { get; set; }
        public string? CreatedUserName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? Name { get; set; }
        public string? UniqueCode { get; set; }
    }
}
