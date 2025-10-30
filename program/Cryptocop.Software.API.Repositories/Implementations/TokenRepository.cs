using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Data;


namespace Cryptocop.Software.API.Repositories.Implementations;

public class TokenRepository : ITokenRepository
{
    private readonly CryptocopDbContext _context;
    public TokenRepository(CryptocopDbContext context) => _context = context;


    public Task<JwtTokenDto> CreateNewTokenAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsTokenBlacklistedAsync(int tokenId)
    {
        throw new NotImplementedException();
    }

    public Task VoidTokenAsync(int tokenId)
    {
        throw new NotImplementedException();
    }
}