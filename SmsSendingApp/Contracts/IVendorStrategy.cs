using SmsSendingApp.Entities;

namespace SmsSendingApp.Contracts;

public interface IVendorStrategy
{
    public short CountryCode { get; }

    /// <summary>
    ///     Sends an sms message to a given phone number.
    /// </summary>
    /// <param name="sms"> <see cref="Sms" /> to be sent.</param>
    public Task SendAsync(Sms sms);
}