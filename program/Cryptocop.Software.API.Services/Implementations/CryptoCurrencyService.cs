using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Services.Implementations;

public class CryptoCurrencyService : ICryptoCurrencyService
{
    public Task<IEnumerable<CryptoCurrencyDto>> GetAvailableCryptocurrenciesAsync()
    {
        // Call the external API and get all cryptocurrencies with fields required for the CryptoCurrencyDto model
        // Deserializes the response to a list - I would advise to use the 
        // HttpResponseMessageExtensions which is located within Helpers/ to deserialize
        // and flatten the response.
        // Return a filtered list where only the available cryptocurrencies BitCoin (BTC),
        // Ethereum (ETH), Tether (USDT) and Chainlink (LINK) are within the list
        
        throw new NotImplementedException();
    }
}