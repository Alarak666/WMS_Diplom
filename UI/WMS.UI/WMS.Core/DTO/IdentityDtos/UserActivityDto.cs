namespace WMS.Core.DTO.IdentityDtos;

public class UserActivityDto
{
    public Guid Id { get; set; }
    public Guid ApplicationUserId { get; set; }
    public string? ActivityDescription { get; set; }
    public DateTime ActivityDate { get; set; }
}