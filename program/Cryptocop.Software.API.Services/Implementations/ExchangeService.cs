using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

using Cryptocop.Software.API.Models;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Services.Helpers;

namespace Cryptocop.Software.API.Services.Implementations;

public class ExchangeService : IExchangeService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    public ExchangeService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _baseUrl = config["MessariApi:BaseUrl"] ?? "https://data.messari.io/api/v2";
    }


    public async Task<Envelope<ExchangeDto>> GetExchangesAsync(int pageNumber = 1)
    {
        // Call the external API with a paginated query and get all exchanges 
        // with fields required for the ExchangeDto model.
        // Deserialize the response to a list - I would advise to use the HttpResponseMessageExtensions 
        // which is located within Helpers/ to deserialize and flatten the response.
        // Create an envelope and add the list to the envelope and return that

        var response = await _httpClient.GetAsync($"{_baseUrl}/markets?page={pageNumber}");
        var exchanges = await response.DeserializeJsonToList<ExchangeDto>(flatten: true); // pancake

        // Add sequential IDs and return in an Envelope
        var list = exchanges
            .Select((e, index) => new ExchangeDto
            {
                Id = index + 1,
                Name = e.Name,
                Slug = e.Slug,
                AssetSymbol = e.AssetSymbol,
                PriceInUsd = e.PriceInUsd,
                LastTrade = e.LastTrade
            })
            .ToList();

        return new Envelope<ExchangeDto>
        {
            PageNumber = pageNumber,
            Items = list
        };
    }
}