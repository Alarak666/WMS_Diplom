namespace WMS.UI.Constant.Enum;

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