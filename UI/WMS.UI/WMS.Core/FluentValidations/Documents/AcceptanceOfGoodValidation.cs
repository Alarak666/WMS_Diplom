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
        RuleFor(x => x.Width).GreaterThan(0).WithMessage("Width must be greater than zero");
        RuleFor(x => x.Height).GreaterThan(0).WithMessage("Height must be greater than zero");
        RuleFor(x => x.Length).GreaterThan(0).WithMessage("Length must be greater than zero");
        RuleFor(x => x.Weight).GreaterThan(0).WithMessage("Weight must be greater than zero");
        RuleFor(x => x.Qty).GreaterThan(0).WithMessage("Qty must be greater than zero");

    }
}
