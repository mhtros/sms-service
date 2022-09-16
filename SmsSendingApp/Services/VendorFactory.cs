using SmsSendingApp.Contracts;

namespace SmsSendingApp.Services;

public sealed class VendorFactory : IVendorFactory
{
    private readonly Func<IEnumerable<IVendorStrategy>> _factory;

    public VendorFactory(Func<IEnumerable<IVendorStrategy>> factory)
    {
        _factory = factory;
    }

    public IVendorStrategy Create(short countryCode)
    {
        var strategies = _factory();
        var strategy = strategies.First(x => x.CountryCode == countryCode);
        return strategy;
    }
}