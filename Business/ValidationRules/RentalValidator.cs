using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(r => r.CarId).NotEmpty().WithMessage("Boş olamaz");
            RuleFor(r => r.CustomerId).NotEmpty().WithMessage("Boş olamaz");
            RuleFor(r => r.RentDate).NotEmpty().WithMessage("Boş olamaz");
        }
    }
}