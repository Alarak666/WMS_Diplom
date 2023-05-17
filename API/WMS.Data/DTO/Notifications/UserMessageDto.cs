using WMS.Data.Constant.Enum;

namespace WMS.Data.DTO.Notifications;

public class UserMessageDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Message { get; set; }
    public UserMessageType Type { get; set; }
    public bool MarkedAsRead { get; set; }
}