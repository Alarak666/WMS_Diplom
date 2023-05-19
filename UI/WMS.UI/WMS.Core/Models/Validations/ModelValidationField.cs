namespace WMS.Core.Models.Validations;

public class ModelValidationField
{
    public string FieldName { get; set; }
    public bool? IsValid { get; set; }
    public string? ValidationMessage { get; set; }
}