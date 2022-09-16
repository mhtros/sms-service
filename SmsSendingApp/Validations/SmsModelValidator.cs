using FluentValidation;
using SmsSendingApp.Models;

namespace SmsSendingApp.Validations;

public class SmsModelValidator : AbstractValidator<SmsModel>
{
    public SmsModelValidator()
    {
        RuleFor(smsModel => smsModel.Message).MaximumLength(Constants.SmsMessageMaxLength);
        RuleFor(smsModel => smsModel.ReceiverCountryCode).NotNull();
        RuleFor(smsModel => smsModel.SenderEmail).NotNull().NotEmpty().EmailAddress();
        RuleFor(smsModel => smsModel.ReceiverNumber).NotNull().NotEmpty();
    }
}