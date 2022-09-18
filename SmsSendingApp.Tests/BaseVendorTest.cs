using Microsoft.Extensions.DependencyInjection;
using Moq;
using SmsSendingApp.Contracts;
using SmsSendingApp.Entities;

namespace SmsSendingApp.Tests;

public class BaseVendorTest
{
    private readonly Mock<IServiceScopeFactory> _serviceScopeFactoryMock = new();
    private readonly Mock<IServiceScope> _serviceScopeMock = new();

    protected readonly Mock<IServiceProvider> ServiceProviderMock = new();

    protected readonly Sms Sms = new()
    {
        Id = default,
        Message = string.Empty,
        Order = null,
        ReceiverCountryCode = (short)Constants.CountryCodes.Cyprus,
        ReceiverNumber = "1122334455",
        RequestId = Guid.NewGuid().ToString(),
        SenderEmail = "email@email.com"
    };

    protected readonly Mock<ISmsRepository> SmsRepositoryMock = new();

    protected BaseVendorTest()
    {
        ServiceProviderMock
            .Setup(x => x.GetService(typeof(ISmsRepository)))
            .Returns(SmsRepositoryMock.Object);

        _serviceScopeMock
            .Setup(x => x.ServiceProvider)
            .Returns(ServiceProviderMock.Object);

        _serviceScopeFactoryMock
            .Setup(x => x.CreateScope())
            .Returns(_serviceScopeMock.Object);

        ServiceProviderMock
            .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
            .Returns(_serviceScopeFactoryMock.Object);
    }
}