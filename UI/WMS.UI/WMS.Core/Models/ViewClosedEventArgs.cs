namespace WMS.Core.Models;

public class ViewClosedEventArgs
{
    public bool Saved { get; set; }
    public object Data { get; set; }
    public Guid? ObjectId { get; set; }
}