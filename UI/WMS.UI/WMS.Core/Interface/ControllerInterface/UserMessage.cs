using WMS.Core.Constants.Enum;

namespace WMS.Core.Interface.ControllerInterface
{
    public class UserMessage
    {
        public UserMessage()
        {
            Date = DateTime.Now;
        }
        public DateTime Date { get; }
        public string Message { get; set; }
        public UserMessageType Type { get; set; }
    }
}
