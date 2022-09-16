namespace SmsSendingApp.Contracts;

public interface IVendorFactory
{
    /// <summary>
    ///     Creates a new <see cref="IVendorStrategy" /> instance based on a given country code number.
    /// </summary>
    /// <param name="countryCode">Country code number (the choice of strategy will be made based on this number).</param>
    /// <returns>Α instance of <see cref="IVendorStrategy" />.</returns>
    public IVendorStrategy Create(short countryCode);
}