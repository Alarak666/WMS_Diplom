using ERP.Core.Entities.Catalogs.CompanyCatalog;
using ERP.Core.Entities.Catalogs.CurrencyCatalog;
using ERP.Core.Entities.Catalogs.ExpenseItemsCatalog;
using ERP.Core.Entities.Catalogs.IncomeItemsCatalog;
using ERP.Core.Entities.Catalogs.WarehouseCatalog;
using WMS.Data.Constant.Enum;
using WMS.Data.Entity.Persons;

namespace WMS.Data.Entity.Identity;

public class ApplicationUserSettings
{
    public Guid Id { get; set; }
    public Guid? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? CurrentLocale { get; set; }
    public string? Timezone { get; set; }
    public VerificationType VerificationType { get; set; }
    public Guid? PositionId { get; set; }
    public Position? Position { get; set; }

    public Guid? DefaultCurrencyId { get; set; }
    public Currency? DefaultCurrency { get; set; }
    public Guid? DefaultCompanyId { get; set; }
    public Company? DefaultCompany { get; set; }
    public Guid? DefaultWarehouseId { get; set; }
    public Warehouse? DefaultWarehouse { get; set; }
    public Guid? DefaultDepartmentId { get; set; }
    public Division? DefaultDepartment { get; set; }

    public Guid? DefaultIncomeItemId { get; set; }
    public IncomeItem? DefaultIncomeItem { get; set; }

    public Guid? DefaultExpenseItemId { get; set; }
    public ExpenseItem? DefaultExpenseItem { get; set; }
}