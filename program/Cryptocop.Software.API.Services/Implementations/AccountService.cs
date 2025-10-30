using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Repositories.Interfaces;


namespace Cryptocop.Software.API.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly IUserRepository _repo;
    public AccountService(IUserRepository repo) => _repo = repo;


    public Task<UserDto> CreateUserAsync(RegisterInputModel inputModel)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> AuthenticateUserAsync(LoginInputModel loginInputModel)
    {
        throw new NotImplementedException();
    }

    public Task LogoutAsync(int tokenId)
    {
        throw new NotImplementedException();
    }
}