using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.BrandId).NotEmpty().WithMessage("Boş olamaz");
            RuleFor(c => c.ColorId).NotEmpty().WithMessage("Boş olamaz");
            RuleFor(c => c.DailyPrice).NotEmpty().WithMessage("Boş olamaz");
            RuleFor(c => c.DailyPrice).GreaterThan(100).WithMessage("Günlük fiyat 100 den büyük olmalı");
            RuleFor(c => c.ModelYear).GreaterThan(2009).WithMessage("Model yılı 2009 dan büyük olmalı");

        }
    }
}