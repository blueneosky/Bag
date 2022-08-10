using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alphonse.WebApi.Dbo;
using FluentValidation;

namespace Alphonse.WebApi.Validators;

public class CallHistoryDboValidator : AbstractValidator<CallHistoryDbo>
{
    public CallHistoryDboValidator()
    {
        RuleFor(ch => ch.UCallNumber).UPhoneNumber();
    }
}