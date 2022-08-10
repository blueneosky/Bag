using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alphonse.WebApi.Dbo;
using FluentValidation;

namespace Alphonse.WebApi.Validators;

public class PhoneNumberDboValidator : AbstractValidator<PhoneNumberDbo>
{
    public PhoneNumberDboValidator()
    {
        RuleFor(pn => pn.Name).NotEmpty().MaximumLength(64);
        RuleFor(pn => pn.UPhoneNumber).UPhoneNumber();
    }
}