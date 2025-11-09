using Microsoft.EntityFrameworkCore;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Data;
using Cryptocop.Software.API.Repositories.Helpers;


namespace Cryptocop.Software.API.Repositories.Implementations;

public class TokenRepository : ITokenRepository
{
    private readonly CryptocopDbContext _context;
    public TokenRepository(CryptocopDbContext context) => _context = context;


    // Create
    public async Task<JwtTokenDto> CreateNewTokenAsync()
    {
        var newToken = new JwtToken
        {
            Blacklisted = false
        };
        
        await _context.JwtTokens.AddAsync(newToken);
        await _context.SaveChangesAsync();

        return newToken.ToDto();
    }

    // is it???
    public async Task<bool> IsTokenBlacklistedAsync(int tokenId)
    {
        var token = await _context.JwtTokens.FirstOrDefaultAsync(t => t.Id == tokenId);
        if (token == null) throw new ArgumentException($"Token with ID: {tokenId} not found");

        return token.Blacklisted;
    }

    public async Task VoidTokenAsync(int tokenId)
    {
        var token = await _context.JwtTokens.FirstOrDefaultAsync(t => t.Id == tokenId);
        if (token == null) throw new ArgumentException($"Token with ID: {tokenId} not found");

        token.Blacklisted = true;

        _context.JwtTokens.Update(token);
        await _context.SaveChangesAsync();
    }
}