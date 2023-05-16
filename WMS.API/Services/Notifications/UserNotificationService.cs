using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WMS.API.Hubs.Notification;
using WMS.Data.Constant.Enum;
using WMS.Data.Context;
using WMS.Data.Entity.Notifications;
using WMS.Data.Interface.ControllerInterface;
using WMS.Data.Middlewares.DescriptionExceptions;

namespace WMS.API.Services.Notifications;

public class UserNotificationService : IUserNotificationService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IHubContext<UserNotificationHub> _hubContext;
    private readonly IMapper _mapper;

    public UserNotificationService(IDbContextFactory<ApplicationDbContext> dbContextFactory,
        IHubContext<UserNotificationHub> hubContext, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _hubContext = hubContext;
        _mapper = mapper;
    }

    public event IUserNotificationService.UpdateMessage? OnMessageAdded;

    public void AddMessage(UserMessage message)
    {
        var context = _dbContextFactory.CreateDbContext();
        context.UserMessages.Add(new UserMessage
        {
            Message = message.Message,
            Type = message.Type,
            Date = message.Date,
            MarkedAsRead = false
        });
        context.SaveChanges();

        _hubContext.Clients.All.SendAsync("ShowUserMessage", message).Wait();
    }

    public void AddErrorMessage(string message)
    {
        AddMessage(new UserMessage
        {
            Message = message,
            Type = UserMessageType.Error,
            Date = DateTime.Now
        });
    }

    public ICollection<UserMessage> GetMessages()
    {
        var _dbContext = _dbContextFactory.CreateDbContext();
        return _dbContext.UserMessages.ProjectTo<UserMessage>(_mapper.ConfigurationProvider).ToList();
    }
    public void AddDocumentSaveFailureMessage(string errorMessage)
    {
        AddMessage(new UserMessage { Message = errorMessage, Type = UserMessageType.Error });
    }

    public void AddDocumentCreateFailureMessage(string errorMessage)
    {
        AddMessage(new UserMessage { Message = errorMessage, Type = UserMessageType.Error });
    }

    public void AddDocumentDeleteFailureMessage(string errorMessage)
    {
        AddMessage(new UserMessage { Message = errorMessage, Type = UserMessageType.Error });
    }

    public void AddDocumentSaveSuccessMessage(string documentPresentationName)
    {
        AddMessage(new UserMessage { Message = documentPresentationName, Type = UserMessageType.Info });
    }

    public void AddDocumentDeleteSuccessMessage(string documentPresentationName)
    {
        AddMessage(new UserMessage { Message = documentPresentationName, Type = UserMessageType.Info });
    }

    public void AddDocumentCreateSuccessMessage(string documentPresentationName)
    {
        AddMessage(new UserMessage { Message = documentPresentationName, Type = UserMessageType.Info });
    }
}