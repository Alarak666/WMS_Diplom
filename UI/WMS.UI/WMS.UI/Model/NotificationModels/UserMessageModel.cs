
using WMS.UI.Constant.Enum;

namespace WMS.UI.Model.NotificationModels;

public class UserMessageModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Message { get; set; }
    public UserMessageType Type { get; set; }
    public bool MarkedAsRead { get; set; }
}