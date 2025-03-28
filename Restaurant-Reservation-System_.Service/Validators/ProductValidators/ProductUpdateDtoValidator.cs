

using FluentValidation;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;
using Restaurant_Reservation_System_.Service.Validators.ProductDetailValidators;

namespace Restaurant_Reservation_System_.Service.Validators.ProductValidators
{
    public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0);

            RuleForEach(x => x.ProductDetails).SetValidator(new ProductDetailUpdateDtoValidator());
        }
    }
}
