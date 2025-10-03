using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Metals2.Tests
{
    public class MetalsApiServiceTestsToChange
    {
        [Fact]
        public async Task GetGoldPriceAsync_ReturnsValue_WhenApiResponseIsValid()
        {
            // Arrange
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(httpMessageHandlerMock.Object);

            var inMemorySettings = new Dictionary<string, string> {
                {"MetalsApi:ApiKey", "test-key"}
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var service = new MetalsApiService(httpClient, configuration);

            // Ici, tu devrais mocker la réponse HTTP pour simuler l'API.
            // Pour un test complet, utilise un framework comme RichardSzalay.MockHttp.

            // Act
            var result = await service.GetGoldPriceAsync();

            // Assert
            // Remplace par une assertion adaptée à ton mock
            Assert.Null(result);
        }
    }
}