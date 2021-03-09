using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.CompanyName).NotEmpty().WithMessage("Boş olamaz");
            RuleFor(c => c.CompanyName).NotEmpty().Length(2);

        }
    }
}