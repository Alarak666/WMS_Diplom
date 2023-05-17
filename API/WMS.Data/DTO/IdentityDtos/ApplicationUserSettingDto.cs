using WMS.Data.Constant.Enum;

namespace WMS.Data.DTO.IdentityDtos;

public class ApplicationUserSettingDto
{
    public Guid Id { get; set; }
    public Guid? ApplicationUserId { get; set; }
    public string? CurrentLocale { get; set; }
    public string? Timezone { get; set; }
    public VerificationType VerificationType { get; set; }
    public Guid? PositionId { get; set; }
}