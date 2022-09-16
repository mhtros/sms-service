using SmsSendingApp.Contracts;
using SmsSendingApp.Services;

namespace SmsSendingApp.Extensions;

public static class VendorFactoryExtension
{
    /// <summary>
    /// Configures <see cref="IVendorStrategy"/> using the <see cref="IVendorFactory"/> factory.
    /// </summary>
    public static void AddVendorFactory(this IServiceCollection services)
    {
        services.AddTransient<IVendorStrategy, SmsVendorGr>();
        services.AddTransient<IVendorStrategy, SmsVendorCy>();
        services.AddTransient<IVendorStrategy, SmsVendorRest>();

        services.AddSingleton<Func<IEnumerable<IVendorStrategy>>>(x =>
            () => x.GetService<IEnumerable<IVendorStrategy>>()!);

        services.AddSingleton<IVendorFactory, VendorFactory>();
    }
}