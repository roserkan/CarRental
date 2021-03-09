using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules
{
    public class ColorValidator : AbstractValidator<Color>
    {
        public ColorValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Boş olamaz");
        }
    }
}