using Microsoft.EntityFrameworkCore;
using WMS.Data.Constant.Enum;

namespace WMS.Data.Middlewares.CustomExceptions;

public class DatabaseSaveFailedException : Exception
{
    public DatabaseSaveFailedException(DbUpdateException ex)
    {
    }

    public GlobalExceptionErrorCode ErrorCode { get; set; } = GlobalExceptionErrorCode.DatabaseSaveError;
}