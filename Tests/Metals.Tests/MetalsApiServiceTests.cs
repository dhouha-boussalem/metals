using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RichardSzalay.MockHttp;
using Xunit;

namespace Metals2.Tests
{
    public class MetalsApiServiceTests
    {
        [Fact]
        public async Task GetGoldPriceAsync_ReturnsValue_WhenApiResponseIsValid()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();

            var jsonResponse = @"{
                ""rates"": { ""XAU"": 2345.67 }
            }";

            mockHttp.When("https://metals-api.com/api/latest*")
                    .Respond("application/json", jsonResponse);

            var httpClient = new HttpClient(mockHttp);

            var inMemorySettings = new Dictionary<string, string> {
                {"MetalsApi:ApiKey", "test-key"}
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var service = new MetalsApiService(httpClient, configuration);

            // Act
            var result = await service.GetGoldPriceAsync();

            // Assert
            Assert.Equal(2345.67m, result);
        }
    }
}