using System.Net;
using System.Net.Http.Json;
using Dapper;
using Microsoft.Extensions.Configuration;
using Moq;
using SmsSendingApp.Data;
using SmsSendingApp.IntegrationTests.SeedData;
using SmsSendingApp.Models;

namespace SmsSendingApp.IntegrationTests;

public class PostSmsTests : IntegrationTest
{
    private readonly DatabaseContext _context;
    private readonly Mock<IConfiguration> _mockConfiguration = new();
    private readonly Mock<IConfigurationSection> _mockConfSection = new();

    public PostSmsTests()
    {
        _mockConfSection
            .SetupGet(m => m[It.Is<string>(s => s == "DefaultConnection")])
            .Returns(ConnectionString);

        _mockConfiguration
            .Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings")))
            .Returns(_mockConfSection.Object);

        _context = new DatabaseContext(_mockConfiguration.Object);
    }

    [Theory]
    [ClassData(typeof(PostSmsTestValidDataGenerator))]
    public async Task Sms_ShouldSavesValidEntities(short countryCode, string message, int entitiesExpected)
    {
        // Arrange

        var sms = new SmsModel
        {
            Message = message,
            ReceiverCountryCode = countryCode,
            ReceiverNumber = "1122334455",
            SenderEmail = "email@email.com"
        };

        // Act

        var response = await TestClient.PostAsJsonAsync("/sms", sms);
        var requestId = await response.Content.ReadFromJsonAsync<string>();

        // Assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(requestId);
        Assert.NotEmpty(requestId);

        const string query = "SELECT COUNT(*) FROM Messages WHERE RequestId = @RequestId";

        using var connection = _context.CreateConnection();
        var count = await connection.QueryFirstAsync<int>(query, new { RequestId = requestId });

        // clean database
        const string deleteQuery = "DELETE FROM Messages WHERE RequestId = @RequestId";
        await connection.ExecuteAsync(deleteQuery, new { RequestId = requestId });

        Assert.Equal(entitiesExpected, count);
    }
}