using WMS.Data.Constant.Enum;
using WMS.Data.DTO.Middlewares;

namespace WMS.Data.Middlewares.CustomExceptions;

public class DocumentValidationException : Exception
{
    public const string MessageError = "Validation errors occurred while posting document with ID {0}";

    public DocumentValidationException(Guid id, IEnumerable<ValidationError> validationErrors)
        : base(string.Format(MessageError, id))
    {
        ValidationErrors = validationErrors;
    }

    public GlobalExceptionErrorCode ErrorCode { get; set; } = GlobalExceptionErrorCode.DocumentNotFound;
    public IEnumerable<ValidationError>? ValidationErrors { get; set; }
}