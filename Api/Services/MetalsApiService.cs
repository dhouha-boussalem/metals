using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class MetalsApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public MetalsApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["MetalsApi:ApiKey"];
    }

    public async Task<decimal?> GetGoldPriceAsync()
    {
        // Exemple d’URL, à adapter selon la documentation Metals-API
        var url = $"https://metals-api.com/api/latest?access_key={_apiKey}&base=USD&symbols=XAU";
        var response = await _httpClient.GetFromJsonAsync<MetalsApiResponse>(url);
        return response?.Rates?.XAU;
    }

    private class MetalsApiResponse
    {
        public RatesData Rates { get; set; }
    }

    private class RatesData
    {
        public decimal XAU { get; set; }
    }
}