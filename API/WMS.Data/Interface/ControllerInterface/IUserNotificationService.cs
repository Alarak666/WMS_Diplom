using System.Linq.Expressions;
using WMS.Data.Entity.Notifications;

namespace WMS.Data.Interface.ControllerInterface;

public interface IUserNotificationService
{
    public delegate Task UpdateMessage(UserMessage msg);

    event UpdateMessage OnMessageAdded;
    void AddMessage(UserMessage message);
    void AddErrorMessage(string message);
    ICollection<UserMessage> GetMessages();
    void AddDocumentSaveFailureMessage(string errorMessage);
    void AddDocumentCreateFailureMessage(string errorMessage);
    void AddDocumentDeleteFailureMessage(string errorMessage);
    void AddDocumentSaveSuccessMessage(string documentPresentationName);
    void AddDocumentDeleteSuccessMessage(string documentPresentationName);
    void AddDocumentCreateSuccessMessage(string documentPresentationName);

}