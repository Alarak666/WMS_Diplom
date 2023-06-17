using FluentValidation;
using WMS.Core.Models.DocumentModels.StockModels;

namespace WMS.Core.FluentValidations.Documents;
public class AcceptanceOfGoodValidation : AbstractValidator<AcceptanceOfGoodDetailViewModel>
{
    public AcceptanceOfGoodValidation()
    {
        RuleFor(x => x.EmployerId).NotNull().NotEmpty().WithMessage("Employee is required");
        RuleFor(x => x.ProductId).NotNull().NotEmpty().WithMessage("Product is required");
        RuleFor(x => x.DateAccepts).NotNull().NotEmpty().WithMessage("Data Accept is required");


    }
}
