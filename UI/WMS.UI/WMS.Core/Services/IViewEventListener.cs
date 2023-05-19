namespace WMS.Core.Services
{
    public interface IViewEventListener
    {
        public delegate Task ViewUpdateRequestEventHandler(object source, string eventId);
        event ViewUpdateRequestEventHandler ViewUpdateRequest;
        void FireEvents(string eventId, Guid documentId);
    }
}
