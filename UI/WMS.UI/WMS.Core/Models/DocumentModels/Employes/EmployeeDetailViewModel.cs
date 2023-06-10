using WMS.Core.Constants.Enum;
using WMS.Core.Interface;

namespace WMS.Core.Models.DocumentModels.Employes
{
    public class EmployeeDetailViewModel: IBaseField
    {
        public EmployeeDetailViewModel()
        {
            CreatedDate = DateTime.Now;
        }
        public Guid Id { get; set; }
        public Guid? CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; } = default;
        public string? UniqueCode { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? DivisionId { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? PersonId { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? Tin { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
    }
}
