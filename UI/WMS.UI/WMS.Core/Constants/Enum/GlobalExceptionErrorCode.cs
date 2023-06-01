namespace WMS.Core.Constants.Enum;

public enum GlobalExceptionErrorCode
{
    DocumentNotFound,
    DocumentValidationFailed,
    Exception,
    DatabaseSaveError,
    UnknownError = 0,
    NotEnoughOrderedStock,
    NotEnoughCostStock,
    ExportException,
    PostFieldException,
    CurrencyAlreadyProcessed
}