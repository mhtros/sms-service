namespace SmsSendingApp.Models;

public class SmsModel
{
    public string Message { get; set; } = string.Empty;
    public short ReceiverCountryCode { get; set; }
    public string SenderEmail { get; set; } = string.Empty;
    public string ReceiverNumber { get; set; } = string.Empty;
}