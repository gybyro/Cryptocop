using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Repositories.Interfaces;


namespace Cryptocop.Software.API.Services.Implementations;

public class TokenService : ITokenService
{
    private readonly ITokenRepository _repo;
    public TokenService(ITokenRepository repo) => _repo = repo;


    public Task<string> GenerateJwtTokenAsync(UserDto user)
    {
        throw new NotImplementedException();
    }
}