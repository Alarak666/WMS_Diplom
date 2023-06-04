namespace WMS.Core.Models.DocumentModels.Divisions
{
    public class DivisionListViewModel
    {
        public Guid Id { get; set; }
        public Guid? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public Guid? ParentDivisionId { get; set; }
        public string? ParentDivisionName { get; set; }
        public Guid? CreatedUserId { get; set; }
        public string? CreatedUserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Name { get; set; }
        public string? UniqueCode { get; set; }
    }
}
