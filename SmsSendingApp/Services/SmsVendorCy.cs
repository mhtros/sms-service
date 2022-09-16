using System.Transactions;
using SmsSendingApp.Contracts;
using SmsSendingApp.Entities;

namespace SmsSendingApp.Services;

public class SmsVendorCy : BaseVendor, IVendorStrategy
{
    public SmsVendorCy(IServiceProvider provider) : base(provider)
    {
    }

    public short CountryCode => (short)Constants.CountryCodes.Cyprus;

    public async Task SendAsync(Sms sms)
    {
        var msgLength = sms.Message.Length;

        CheckIfExceedingMaxLength(msgLength);

        if (msgLength <= Constants.SmsMessageChunkSize)
        {
            await SmsRepository.SaveAsync(sms);
        }
        else
        {
            var messageParts = sms.Message
                .Chunk(Constants.SmsMessageChunkSize)
                .Select(chunk => new string(chunk))
                .ToArray();

            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            for (short i = 0; i < messageParts.Length; i++)
            {
                sms.Id = default;
                sms.Order = i;
                sms.Message = messageParts[i];

                await SmsRepository.SaveAsync(sms);
            }

            transactionScope.Complete();
        }
    }
}