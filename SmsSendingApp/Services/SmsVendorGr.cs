using System.Text.RegularExpressions;
using SmsSendingApp.Contracts;
using SmsSendingApp.Entities;
using SmsSendingApp.Exceptions;

namespace SmsSendingApp.Services;

public class SmsVendorGr : BaseVendor, IVendorStrategy
{
    private const string OnlyGreekRegex = @"^[(0-9.!@?#""$%&:;() *\+,\/;\-=[\\\]\^_{|}<>\p{IsGreek}+(\s)?)+]*$";

    public SmsVendorGr(IServiceProvider provider) : base(provider)
    {
    }

    public short CountryCode => (short)Constants.CountryCodes.Greece;

    public async Task SendAsync(Sms sms)
    {
        CheckIfExceedingMaxLength(sms.Message.Length);

        var onlyGreekCharacters = Regex.IsMatch(sms.Message, OnlyGreekRegex);
        if (onlyGreekCharacters is false) throw new MessageContainsNonGreekCharactersException(sms.Message);

        await SmsRepository.SaveAsync(sms);
    }
}