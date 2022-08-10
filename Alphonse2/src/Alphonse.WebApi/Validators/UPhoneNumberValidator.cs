using FluentValidation;

namespace Alphonse.WebApi.Validators
{
    public static class UPhoneNumberValidator
    {
        public static IRuleBuilder<T, string> UPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder.NotNull().NullableUPhoneNumber();

        public static IRuleBuilder<T, string> NullableUPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Length(3, 30)
                .Custom(Validate);

            //======================================================

            void Validate(string value, ValidationContext<T> context)
            {
                if (value is null)
                    return;

                if(!value.StartsWith('+'))
                    context.AddFailure($"'{context.DisplayName}' must start with a '+'");

                if(!value.Skip(1).All(char.IsDigit))
                    context.AddFailure($"'{context.DisplayName}' must contains only digits after '+'");
            }
        }
    }
}