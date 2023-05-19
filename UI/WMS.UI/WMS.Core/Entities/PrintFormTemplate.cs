using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WMS.Core.Entities
{
    public class PrintFormTemplate
    {
        public Guid Id { get; set; }
        public string? TemplateName { get; set; }
        public byte[]? TemplateBody { get; set; }
    }
}
