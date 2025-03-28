
using FluentValidation;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Validators.CategoryDetailValidators;

namespace Restaurant_Reservation_System_.Service.Validators.CategoryValidators
{
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleForEach(x => x.CategoryDetails).SetValidator(new CategoryDetailCreateDtoValidator());
        }
    }
}
