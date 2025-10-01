using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

public class GoldPriceServiceTests
{
    [Fact]
    public async Task GetGoldPriceAsync_ReturnsPrice_WhenHtmlIsValid()
    {
        // Arrange
        var html = @"<fin-streamer data-field=""regularMarketPrice"">2,345.67</fin-streamer>";
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(html),
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new GoldPriceService(httpClient);

        // Act
        var result = await service.GetGoldPriceAsync();

        // Assert
        Assert.Equal(2345.67m, result);
    }

    [Fact]
    public async Task GetGoldPriceAsync_ReturnsNull_WhenHtmlIsInvalid()
    {
        // Arrange
        var html = "<html><body>No price here</body></html>";
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(html),
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new GoldPriceService(httpClient);

        // Act
        var result = await service.GetGoldPriceAsync();

        // Assert
        Assert.Null(result);
    }
}