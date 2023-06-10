using System.Net;
using System.Text;
using Newtonsoft.Json;
using WMS.Core.Constants.Enum;
using WMS.Core.DTO.Middlewares;
using WMS.Core.Interface.ControllerInterface;

namespace WMS.API.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger,
        RequestDelegate next, IServiceProvider serviceProvider
    )
    {
        _logger = logger;
        _next = next;
        _serviceProvider = serviceProvider;
    }

    private ErrorResponseDto GetErrorResponseDto(Exception ex)
    {
        _logger.LogError(ex, ex.Message);
        using (var scope = _serviceProvider.CreateScope())
        {
            var userNotificationService = scope.ServiceProvider.GetRequiredService<IUserNotificationService>();

            var errorResponseDto = new ErrorResponseDto
            {
                ErrorCode = GlobalExceptionErrorCode.UnknownError,
                ErrorMessage = ex.Message,
                Success = false
            };
            switch (ex)
            {
                case Exception:
                    errorResponseDto.ErrorCode = GlobalExceptionErrorCode.Exception;
                    errorResponseDto.ErrorMessage = "Exception";
                    errorResponseDto.DetailException = ex.Message;
                    break;
                default:
                    errorResponseDto.ErrorMessage = "Unknown error";
                    break;
            }

            return errorResponseDto;
        }
    }

    public async Task WriteDtoInResponse(HttpContext context, ErrorResponseDto errorResponseDto)
    {
        context.Response.StatusCode = (int)errorResponseDto.StatusCode;
        context.Response.ContentType = "application/json";
        var jsonResponse = JsonConvert.SerializeObject(errorResponseDto);
        var jsonBytes = Encoding.UTF8.GetBytes(jsonResponse);
        await context.Response.Body.WriteAsync(jsonBytes, 0, jsonBytes.Length);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await WriteDtoInResponse(context, GetErrorResponseDto(ex));
        }
    }
}