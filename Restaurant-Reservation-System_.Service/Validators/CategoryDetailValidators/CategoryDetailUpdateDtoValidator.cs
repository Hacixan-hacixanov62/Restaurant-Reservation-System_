using FluentValidation;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDetailDtos;


namespace Restaurant_Reservation_System_.Service.Validators.CategoryDetailValidators
{
    public class CategoryDetailUpdateDtoValidator : AbstractValidator<CategoryDetailUpdateDto>
    {
        public CategoryDetailUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(32);

        }
    }
}
