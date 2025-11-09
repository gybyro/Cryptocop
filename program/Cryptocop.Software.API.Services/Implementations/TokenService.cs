
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Cryptocop.Software.API;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Services.Helpers;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Repositories.Interfaces;


namespace Cryptocop.Software.API.Services.Implementations;

public class TokenService : ITokenService
{
    private readonly ITokenRepository _repo;
    private readonly JwtSettings _jwtSettings;
    public TokenService(ITokenRepository repo, IOptions<JwtSettings> jwtOptions)
    {
        _repo = repo;
        _jwtSettings = jwtOptions.Value;
    }


    public Task<string> GenerateJwtTokenAsync(UserDto user)
    {
        // Creates a valid JWT token and assigns the information stored within the UserDto
        // model as claims and returns the newly created token

        if (user == null) throw new ArgumentNullException(nameof(user));
        if (user.TokenId <= 0) throw new ArgumentException("A persisted token id is required to generate a JWT token.", nameof(user));
        

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Sid, user.TokenId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifetimeMinutes),
            signingCredentials: creds
        );

        var encodedToken = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        return Task.FromResult(encodedToken);
    }
}