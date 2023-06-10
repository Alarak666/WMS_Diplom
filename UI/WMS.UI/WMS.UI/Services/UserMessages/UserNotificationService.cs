using WMS.Core.Constants.Enum;
using WMS.Core.Interface.ControllerInterface;

namespace WMS.UI.Services.UserMessages;

public class UserNotificationService : IUserNotificationService
{
    public event IUserNotificationService.UpdateMessage OnMessageAdded;
    
    private ICollection<UserMessage> _messages = new List<UserMessage>();

    public UserNotificationService()
    {
        
    }

    public void AddMessage(UserMessage message)
    {
        _messages.Add(message);
        OnMessageAdded?.Invoke(message);
    }

    public void AddErrorMessage(string message)
    {
        throw new NotImplementedException();
    }

    public ICollection<UserMessage> GetMessages()
    {
        return _messages;
    }

    public void AddDocumentCancelFailureMessage(string errorMessage)
    {
        AddMessage(new UserMessage()
        {
            Message = $"Document post canceling failure!",
            Type = UserMessageType.Error
        });
        AddMessage(new UserMessage()
        {
            Message = errorMessage,
            Type = UserMessageType.Error
        });
    }
    
    public void AddDocumentPostFailureMessage(string errorMessage)
    {
        AddMessage(new UserMessage()
        {
            Message = $"Document posting failure!",
            Type = UserMessageType.Error
        });
        AddMessage(new UserMessage()
        {
            Message = errorMessage,
            Type = UserMessageType.Error
        });
    }
    public void AddDocumentSaveFailureMessage(string errorMessage)
    {
        AddMessage(new UserMessage()
        {
            Message = $"Document saving failure!",
            Type = UserMessageType.Error
        });
        AddMessage(new UserMessage()
        {
            Message = errorMessage,
            Type = UserMessageType.Error
        });
    }
    public void AddDocumentCreateFailureMessage(string errorMessage)
    {
        AddMessage(new UserMessage()
        {
            Message = $"Document creating failure!",
            Type = UserMessageType.Error
        });
        AddMessage(new UserMessage()
        {
            Message = errorMessage,
            Type = UserMessageType.Error
        });
    }
    public void AddDocumentDeleteFailureMessage(string errorMessage)
    {
        AddMessage(new UserMessage()
        {
            Message = $"Document deleting failure!",
            Type = UserMessageType.Error
        });
        AddMessage(new UserMessage()
        {
            Message = errorMessage,
            Type = UserMessageType.Error
        });
    }

    public void AddDocumentCancelSuccessMessage(string documentPresentationName)
    {
        AddMessage(new UserMessage()
        {
            Message = $"Document {documentPresentationName} was canceled!",
            Type = UserMessageType.Information
        });

    }
    public void AddDocumentSaveSuccessMessage(string documentPresentationName)
    {
        AddMessage(new UserMessage()
        {
            Message = $"Document {documentPresentationName} was saved!",
            Type = UserMessageType.Information
        });

    }
    public void AddDocumentDeleteSuccessMessage(string documentPresentationName)
    {
        AddMessage(new UserMessage()
        {
            Message = $"Document {documentPresentationName} was deleted!",
            Type = UserMessageType.Information
        });

    }
    public void AddDocumentCreateSuccessMessage(string documentPresentationName)
    {
        AddMessage(new UserMessage()
        {
            Message = $"Document {documentPresentationName} was created!",
            Type = UserMessageType.Information
        });

    }
    public void AddDocumentPostSuccessMessage(string documentPresentationName)
    {
        AddMessage(new UserMessage()
        {
            Message = $"Document {documentPresentationName} was posted!",
            Type = UserMessageType.Information
        });

    }
}