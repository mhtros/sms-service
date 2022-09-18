using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace SmsSendingApp.IntegrationTests;

public class IntegrationTest
{
    protected readonly HttpClient TestClient;

    // Must be the same as the Connection string inside the appsettings.json file
    // CATION: Use ONLY with development database.
    protected const string ConnectionString = "enter connection string here...";

    protected IntegrationTest()
    {
        var projectDir = Directory.GetCurrentDirectory();
        var configPath = Path.Combine(projectDir, "appsettings.json");

        var appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration((_, configurationBuilder) =>
            {
                configurationBuilder.AddJsonFile(configPath);
            });
        });

        TestClient = appFactory.CreateClient();
    }
}