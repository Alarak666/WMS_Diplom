using WMS.UI.Model.BaseClassModels;

namespace WMS.UI.Model.DivisionModels;

public class DivisionModel : BaseCatalogModel
{
    public Guid? ParentDivisionId { get; set; }
}