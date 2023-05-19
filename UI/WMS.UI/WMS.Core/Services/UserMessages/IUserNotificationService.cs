using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Core.Services.UserMessages
{
    public interface IUserNotificationService
    {
        public delegate Task UpdateMessage(UserMessage msg);
        event UpdateMessage OnMessageAdded;
        void AddMessage(UserMessage message);
        ICollection<UserMessage> GetMessages();

        void AddDocumentCancelFailureMessage(string errorMessage);
        void AddDocumentPostFailureMessage(string errorMessage);
        void AddDocumentPostSuccessMessage(string documentPresentationName);
        void AddDocumentSaveFailureMessage(string errorMessage);
        void AddDocumentCreateFailureMessage(string errorMessage);
        void AddDocumentDeleteFailureMessage(string errorMessage);
        void AddDocumentSaveSuccessMessage(string documentPresentationName);
        void AddDocumentDeleteSuccessMessage(string documentPresentationName);
        void AddDocumentCreateSuccessMessage(string documentPresentationName);
        void AddDocumentCancelSuccessMessage(string documentPresentationName);

    }
}
