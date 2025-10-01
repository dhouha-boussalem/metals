using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class GoldPriceController : ControllerBase
{
    private readonly GoldPriceService _goldPriceService;

    public GoldPriceController(GoldPriceService goldPriceService)
    {
        _goldPriceService = goldPriceService;
    }

    [HttpGet("/gold-price")]
    public async Task<IActionResult> GetGoldPrice()
    {
        var price = await _goldPriceService.GetGoldPriceAsync();
        if (price == null)
            return StatusCode(502, "Impossible de récupérer le prix de l'or.");

        return Ok(new { goldPrice = price });
    }
}