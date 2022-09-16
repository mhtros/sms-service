using SmsSendingApp.Entities;

namespace SmsSendingApp.Contracts;

public interface ISmsRepository
{
    /// <summary>
    ///     Saves a <see cref="Sms" /> entity to database.
    /// </summary>
    /// <param name="sms"><see cref="Sms" /> to be saved.</param>
    /// <returns>The newly created entity Id.</returns>
    public Task<int> SaveAsync(Sms sms);
}