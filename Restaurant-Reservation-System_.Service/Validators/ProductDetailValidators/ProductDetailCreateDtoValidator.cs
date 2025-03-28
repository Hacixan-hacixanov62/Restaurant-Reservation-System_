using FluentValidation;
using Restaurant_Reservation_System_.Service.Dtos.ProductDetailDtos;


namespace Restaurant_Reservation_System_.Service.Validators.ProductDetailValidators
{
    public class ProductDetailCreateDtoValidator : AbstractValidator<ProductDetailCreateDto>
    {
        public ProductDetailCreateDtoValidator()
        {
            RuleFor(x => x.Description).NotEmpty().MaximumLength(5000).MinimumLength(1);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(128).MinimumLength(2);
        }
    }
}
