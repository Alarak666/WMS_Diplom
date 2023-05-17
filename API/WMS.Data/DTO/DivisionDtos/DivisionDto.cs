using WMS.Data.DTO.BaseClassDtos;

namespace WMS.Data.DTO.DivisionDtos;

public class DivisionDto : BaseCatalogDto
{
    public Guid? ParentDivisionId { get; set; }
}