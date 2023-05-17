using System.Net;
using WMS.Data.Constant.Enum;
using WMS.Data.Middlewares.DescriptionExceptions;

namespace WMS.Data.Middlewares;

public class ErrorResponseDto
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public string DetailException { get; set; } = string.Empty;
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
    public GlobalExceptionErrorCode ErrorCode { get; set; } = default;

    public IEnumerable<ValidationErrorDescription>? ValidationErrors { get; set; } =
        new List<ValidationErrorDescription>();

    public IEnumerable<PostDocumentErrorDescription>? PostErrors { get; set; } =
        new List<PostDocumentErrorDescription>();
}