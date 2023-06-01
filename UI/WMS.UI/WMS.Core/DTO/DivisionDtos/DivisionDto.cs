using WMS.Core.DTO.BaseClassDtos;

namespace WMS.Core.DTO.DivisionDtos;

public class DivisionDto : BaseCatalogDto
{
    public Guid? ParentDivisionId { get; set; }
}