

using FluentValidation;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;
using Restaurant_Reservation_System_.Service.Validators.ProductDetailValidators;

namespace Restaurant_Reservation_System_.Service.Validators.ProductValidators
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.MainImage).NotNull();
            RuleFor(x => x.Price).GreaterThan(0);

            RuleForEach(x => x.Images).NotNull().NotEmpty();
            RuleForEach(x => x.ProductDetails).SetValidator(new ProductDetailCreateDtoValidator());
        }
    }
}
