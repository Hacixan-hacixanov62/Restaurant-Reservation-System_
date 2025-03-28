using FluentValidation;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Validators.CategoryDetailValidators;


namespace Restaurant.BLL.Validators.CategoryValidators
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleForEach(x => x.CategoryDetails).SetValidator(new CategoryDetailUpdateDtoValidator());
        }
    }
}
