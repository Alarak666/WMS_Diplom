namespace WMS.Core.DTO.Middlewares
{
    public class ValidationError
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public string? FieldName { get; set; }
        public string? TableName { get; set; }
        public int LineIndex { get; set; }
    }
}
