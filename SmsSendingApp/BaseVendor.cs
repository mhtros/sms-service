using SmsSendingApp.Contracts;

namespace SmsSendingApp;

public abstract class BaseVendor
{
    protected readonly ISmsRepository SmsRepository;

    protected BaseVendor(IServiceProvider provider)
    {
        // Passing the ISmsRepository dependency using a newly created scope with
        // the service Locator pattern. Doing that will ensure that the ISmsRepository
        // instances will be refreshed every time the BaseVendor class is initialized
        // and there will be no captured dependencies (Scopes overlapping).
        using var scope = provider.CreateScope();
        SmsRepository = scope.ServiceProvider.GetRequiredService<ISmsRepository>();
    }

    protected static void CheckIfExceedingMaxLength(int msgLength)
    {
        if (msgLength > Constants.SmsMessageMaxLength)

            throw new InvalidDataException(
                $"Message exceeds the max length. [Max: {Constants.SmsMessageMaxLength}] [Message:{msgLength}]");
    }
}