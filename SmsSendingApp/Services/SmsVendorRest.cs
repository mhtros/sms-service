using SmsSendingApp.Contracts;
using SmsSendingApp.Entities;

namespace SmsSendingApp.Services;

public class SmsVendorRest : BaseVendor, IVendorStrategy
{
    public SmsVendorRest(IServiceProvider provider) : base(provider)
    {
    }

    public short CountryCode => -1; // wild card.

    public async Task SendAsync(Sms sms)
    {
        CheckIfExceedingMaxLength(sms.Message.Length);
        await SmsRepository.SaveAsync(sms);
    }
}