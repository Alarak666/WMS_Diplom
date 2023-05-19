namespace WMS.Core.Services.UserMessages
{
    public class UserMessage
    {
        public UserMessage()
        {
            Date = System.DateTime.Now;
        }
        public DateTime Date { get; }
        public string Message { get; set; }
        public UserMessageType Type { get; set; }
    }
}
