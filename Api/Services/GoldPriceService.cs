using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class GoldPriceService
{
    private readonly HttpClient _httpClient;

    public GoldPriceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal?> GetGoldPriceAsync()
    {
        // Récupère la page Yahoo Finance pour l'or (XAUUSD)
        var url = "https://finance.yahoo.com/quote/XAUUSD=X/";
        var html = await _httpClient.GetStringAsync(url);

        // Extraction simple du prix avec une expression régulière
        var match = Regex.Match(html, @"<fin-streamer[^>]*data-field=""regularMarketPrice""[^>]*>([\d.,]+)</fin-streamer>");
        if (match.Success && decimal.TryParse(match.Groups[1].Value.Replace(",", ""), out var price))
        {
            return price;
        }
        return null;
    }
}