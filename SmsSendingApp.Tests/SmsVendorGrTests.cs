using Moq;
using SmsSendingApp.Exceptions;
using SmsSendingApp.Services;

namespace SmsSendingApp.Tests;

public class SmsVendorGrTests : BaseVendorTest
{
    [Fact]
    public async Task SendAsync_ShouldThrowExceptionWhenMessageContainsNonGreekCharacters()
    {
        // Arrange

        Sms.Id = 1;
        Sms.Message = "Non Greek characters";
        SmsRepositoryMock.Setup(x => x.SaveAsync(Sms)).ReturnsAsync(1);

        var sut = new SmsVendorGr(ServiceProviderMock.Object);

        // Assert

        await Assert.ThrowsAsync<MessageContainsNonGreekCharactersException>(async () => await sut.SendAsync(Sms));
    }

    [Fact]
    public async Task SendAsync_ShouldThrowExceptionWhenMessageExceedsMaximumLength()
    {
        // Arrange

        Sms.Id = 1;
        const int length = Constants.SmsMessageMaxLength + 10;
        Sms.Message = string.Concat(Enumerable.Repeat("α", length));
        SmsRepositoryMock.Setup(x => x.SaveAsync(Sms)).ReturnsAsync(1);

        var sut = new SmsVendorGr(ServiceProviderMock.Object);

        // Assert

        var exception = await Assert.ThrowsAsync<InvalidDataException>(async () => await sut.SendAsync(Sms));
        Assert.Equal($"Message exceeds the max length. [Max: {Constants.SmsMessageMaxLength}] [Message:{length}]",
            exception.Message);
    }
}