namespace WMS.Core.Models.DocumentModels.Positions
{
    public class PositionListViewModel
    {
        public string? Description { get; set; }
        public DateTime DateOfApproval { get; set; }
        public bool PositionApproved { get; set; }
        public decimal MainSalary { get; set; }
        public string? DivisionName { get; set; }
        public Guid? DivisionId { get; set; }
        public Guid Id { get; set; }
        public string? CreatedUserName { get; set; }
        public Guid? CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Name { get; set; }
        public string? UniqueCode { get; set; }
    }
}
