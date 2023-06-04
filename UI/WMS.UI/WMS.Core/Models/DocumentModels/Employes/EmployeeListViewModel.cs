using WMS.Core.Constants.Enum;

namespace WMS.Core.Models.DocumentModels.Employes
{
    public class EmployeeListViewModel
    {
        public decimal CompanyInsurance { get; set; }
        public decimal CompanySalary { get; set; }
        public string? DivisionName { get; set; }
        public Guid? DivisionId { get; set; }
        public string? PositionName { get; set; }
        public Guid? PositionId { get; set; }
        public string? PersonName { get; set; }
        public Guid? PersonId { get; set; }
        public string? CompanyName { get; set; }
        public Guid? CompanyId { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? Tin { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public Guid Id { get; set; }
        public string? CreatedUserName { get; set; }
        public Guid? CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Name { get; set; }
        public string? UniqueCode { get; set; }
    }
}
