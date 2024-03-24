using FluentValidation;
using Minimal.API.NET8.Models.DTO;

namespace Minimal.API.NET8.Validations;

public class CouponUpdateValidation : AbstractValidator<CouponUpdateDTO>
{
    public CouponUpdateValidation()
    {
        RuleFor(model => model.Id).NotEmpty().GreaterThan(0);
        RuleFor(model => model.Name).NotEmpty();
        RuleFor(model => model.Percent).InclusiveBetween(1, 100);
    }
}
