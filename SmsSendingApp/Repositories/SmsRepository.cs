using Dapper;
using SmsSendingApp.Contracts;
using SmsSendingApp.Data;
using SmsSendingApp.Entities;

namespace SmsSendingApp.Repositories;

public class SmsRepository : ISmsRepository
{
    private readonly DatabaseContext _context;

    public SmsRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> SaveAsync(Sms sms)
    {
        const string query =
            "INSERT INTO Messages([Order], [CountryCode], [RequestId], [SenderEmail], [Message], [Phone]) " +
            "OUTPUT INSERTED.Id " +
            "VALUES (@Order, @ReceiverCountryCode, @RequestId, @SenderEmail, @Message, @ReceiverNumber);";

        using var connection = _context.CreateConnection();
        var id = await connection.QuerySingleAsync<int>(query, sms);

        return id;
    }
}