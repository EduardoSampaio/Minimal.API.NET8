using FluentValidation;
using Minimal.API.NET8.Models.DTO;

namespace Minimal.API.NET8.Validations;

public class CouponCreateValidation : AbstractValidator<CouponCreateDTO>
{
    public CouponCreateValidation()
    {
        RuleFor(model => model.Name).NotEmpty();
        RuleFor(model => model.Percent).InclusiveBetween(1, 100);
    }
}
