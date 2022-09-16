namespace SmsSendingApp.Entities;

public class Sms
{
    public int Id { get; set; }
    public short? Order { get; set; }
    public string Message { get; set; } = string.Empty;
    public short ReceiverCountryCode { get; set; }
    public string RequestId { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string ReceiverNumber { get; set; } = string.Empty;
}