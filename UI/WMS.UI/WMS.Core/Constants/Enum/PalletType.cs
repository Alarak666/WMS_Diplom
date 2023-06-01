public enum PalletType
{
    Standard,
    Euro,
    Industrial,
    Custom
}

public interface IReferenceType
{
    Guid Id
    {
        get;
        set;
    }
}