using FluentValidation;
using IgorBryt.Store.BLL.Models;

namespace IgorBryt.Store.BLL.Validation;

public class ProductCategoryValidator : AbstractValidator<ProductCategoryModel>
{
    public ProductCategoryValidator()
    {
        RuleFor(x => x.CategoryName)
            .NotEmpty()
            .WithMessage($"The property \"{nameof(ProductCategoryModel.CategoryName)}\" cannot be empty");
    }
}
