namespace SmsSendingApp;

public static class Constants
{
    public const ushort SmsMessageMaxLength = 480;
    public const ushort SmsMessageChunkSize = 160;

    public enum CountryCodes
    {
        Greece = 30,
        Cyprus = 357
    }
}