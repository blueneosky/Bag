using System.Reflection;
using FluentValidation;

namespace Alphonse.WebApi.Setup
{
    public static class ValidatorsSetup
    {
        public static void ConfigureValidators(this WebApplicationBuilder builder)
        {
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Singleton);
        }

        public static void AddValidator<T, TValidator>(this WebApplicationBuilder builder, ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TValidator : IValidator<T>
            => builder.Services.Add(new ServiceDescriptor(typeof(IValidator<T>), typeof(TValidator), lifetime));
    }
}