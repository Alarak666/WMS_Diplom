using FluentValidation;
using WMS.Core.Models.DocumentModels.StockModels;

namespace WMS.Core.FluentValidations.Documents;
public class AcceptanceOfGoodValidation : AbstractValidator<AcceptanceOfGoodDetailViewModel>
{
    public AcceptanceOfGoodValidation()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.UniqueCode).NotNull().NotEmpty().WithMessage("UniqueCode is required");
    }
}
