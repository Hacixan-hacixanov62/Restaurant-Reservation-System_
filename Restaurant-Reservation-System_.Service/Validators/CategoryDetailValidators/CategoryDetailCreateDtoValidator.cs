

using FluentValidation;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDetailDtos;

namespace Restaurant_Reservation_System_.Service.Validators.CategoryDetailValidators
{
    public class CategoryDetailCreateDtoValidator : AbstractValidator<CategoryDetailCreateDto>
    {
        public CategoryDetailCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(32);

        }
    }
}
