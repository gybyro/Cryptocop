using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Services.Helpers;

namespace Cryptocop.Software.API.Services.Implementations;

public class CryptoCurrencyService : ICryptoCurrencyService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    public CryptoCurrencyService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _baseUrl = config["MessariApi:BaseUrl"] ?? "https://data.messari.io/api/v2";
    }


    public async Task<IEnumerable<CryptoCurrencyDto>> GetAvailableCryptocurrenciesAsync()
    {
        // Call the external API and get all cryptocurrencies with fields required for the CryptoCurrencyDto model
        // Deserializes the response to a list - I would advise to use the 
        // HttpResponseMessageExtensions which is located within Helpers/ to deserialize
        // and flatten the response.
        // Return a filtered list where only the available cryptocurrencies BitCoin (BTC),
        // Ethereum (ETH), Tether (USDT) and Chainlink (LINK) are within the list

        var response = await _httpClient.GetAsync($"{_baseUrl}/assets");
        var allAssets = await response.DeserializeJsonToList<CryptoCurrencyDto>(flatten: true); // flat

        // filter for only BTC, ETH, USDT, LINK
        var allowedSymbols = new[] { "BTC", "ETH", "USDT", "LINK" };
        var filtered = allAssets
            .Where(a => allowedSymbols.Contains(a.Symbol.ToUpper()))
            .Select((a, index) => new CryptoCurrencyDto
            {
                Id = index + 1,
                Symbol = a.Symbol,
                Name = a.Name,
                Slug = a.Slug,
                PriceInUsd = a.PriceInUsd,
                ProjectDetails = a.ProjectDetails ?? string.Empty
            })
            .ToList();

        return filtered;
    }
}