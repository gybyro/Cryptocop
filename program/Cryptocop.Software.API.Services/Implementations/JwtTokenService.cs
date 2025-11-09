using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Repositories.Interfaces;

namespace Cryptocop.Software.API.Services.Implementations;

public class JwtTokenService : IJwtTokenService
{
    private readonly ITokenRepository _repo;
    public JwtTokenService(ITokenRepository repo) => _repo = repo;
    

    public Task<bool> IsTokenBlacklistedAsync(int tokenId)
    {
        return _repo.IsTokenBlacklistedAsync(tokenId);
    }
}