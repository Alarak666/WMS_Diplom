using WMS.Data.Constant.Enum;

namespace WMS.Data.Middlewares.CustomExceptions;

public class DocumentNotFoundException : Exception
{
    public const string MessageError = "Document with ID {0} not found.";

    public DocumentNotFoundException(Guid id)
        : base(string.Format(MessageError, id))
    {
    }

    public GlobalExceptionErrorCode ErrorCode { get; set; } = GlobalExceptionErrorCode.DocumentNotFound;
}