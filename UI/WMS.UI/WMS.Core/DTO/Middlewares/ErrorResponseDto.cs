using System.Net;
using WMS.Core.Constants.Enum;

namespace WMS.Core.DTO.Middlewares;

public class ErrorResponseDto
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public string DetailException { get; set; } = string.Empty;
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
    public GlobalExceptionErrorCode ErrorCode { get; set; } = default;
}