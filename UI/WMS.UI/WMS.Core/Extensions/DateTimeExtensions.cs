namespace WMS.Core.Extensions;

public static class DateTimeExtensions
{   
    public static DateTime StartOfMonth(this DateTime @this)
    {
        return new DateTime(@this.Year, @this.Month, 1);
    }
    
    public static DateTime EndOfMonth(this DateTime @this)
    {
        return new DateTime(@this.Year, @this.Month, 1).AddMonths(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
    }
}