using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Repositories.Interfaces;

namespace Cryptocop.Software.API.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;
    public OrderService(IOrderRepository repo) => _repo = repo;


    public Task<IEnumerable<OrderDto>> GetOrdersAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task CreateNewOrderAsync(string email, OrderInputModel order)
    {
        throw new NotImplementedException();
    }
}