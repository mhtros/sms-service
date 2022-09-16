using SmsSendingApp.Contracts;

namespace SmsSendingApp.Services;

public sealed class VendorResolver : IVendorResolver
{
    private readonly IVendorFactory _vendorFactory;

    public VendorResolver(IVendorFactory vendorFactory)
    {
        _vendorFactory = vendorFactory;
    }

    public IVendorStrategy ResolveStrategy(short countryCode)
    {
        const short wildCard = -1;

        return Enum.IsDefined(typeof(Constants.CountryCodes), (int)countryCode)
            ? _vendorFactory.Create(countryCode)
            : _vendorFactory.Create(wildCard);
    }
}