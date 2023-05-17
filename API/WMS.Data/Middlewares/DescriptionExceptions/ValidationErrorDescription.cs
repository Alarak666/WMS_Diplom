namespace WMS.Data.Middlewares.DescriptionExceptions;

public class ValidationErrorDescription
{
    public string FieldName { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}