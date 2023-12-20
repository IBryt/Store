using FluentValidation;
using IgorBryt.Store.BLL.Models;

namespace Business.Validation;

public class ProductValidator : AbstractValidator<ProductModel>
{
    public ProductValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage($"The property \"{nameof(ProductModel.Description)}\" cannot be empty");

        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage($"The property \"{nameof(ProductModel.ProductName)}\" cannot be empty");

        RuleFor(x => x.Price)
            .Must((p) => p > 0)
            .WithMessage("The price cannot be negative");
    }
}
