using WMS.Core.Constants.Enum;

namespace WMS.Core.Models.Report
{
    public class EmployeeReportDto
    {
        public Guid? DivisionId { get; set; }
        public string? DivisionName { get; set; }
        public Guid? PositionId { get; set; }
        public string? PositionName { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? Tin { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
    }
}
