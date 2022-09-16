namespace SmsSendingApp.Contracts;

public interface IVendorResolver
{
    /// <summary>
    ///     Resolves at runtime which <see cref="IVendorStrategy" /> class instance will be instantiate based on a given
    ///     country code number.
    /// </summary>
    /// <param name="countryCode">Country code number (the choice of strategy will be made based on this number).</param>
    /// <returns>Α instance of <see cref="IVendorStrategy" />.</returns>
    public IVendorStrategy ResolveStrategy(short countryCode);
}