using System.Runtime.CompilerServices;
using WMS.Core.Constants.Enum;
using WMS.Core.Interface;

namespace WMS.Core.Models.DocumentModels.Employes
{
    public class EmployeeListViewModel : IBaseField
    {
        public string? DivisionName { get; set; }
        public Guid? DivisionId { get; set; }
        public string? MiddleName { get; set; }
        public string? PositionName { get; set; }
        public Guid? PositionId { get; set; }
        public string? PersonName { get; set; }
        public Guid? PersonId { get; set; }
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
        public string? FullName => $"{this.FirstName} {MiddleName} {LastName}";
        public string? UniqueCode { get; set; }
    }
}
