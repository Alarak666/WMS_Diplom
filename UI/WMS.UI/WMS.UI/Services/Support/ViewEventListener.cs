using WMS.Core.Services;

namespace WMS.UI.Services.Support
{
    public class ViewEventListener : IViewEventListener
    {
        public event IViewEventListener.ViewUpdateRequestEventHandler ViewUpdateRequest;

        public void FireEvents(string eventId, Guid documentId)
        {
            ViewUpdateRequest?.Invoke(this, eventId);
        }
    }
}
