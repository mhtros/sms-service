using FluentValidation;
using FluentValidation.AspNetCore;
using SmsSendingApp.Models;

namespace SmsSendingApp.Extensions;

public static class ValidatorsExtension
{
    /// <summary>
    ///     FluentValidation package wrapper.
    /// </summary>
    public static void AddModelValidations(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<SmsModel>();
    }
}