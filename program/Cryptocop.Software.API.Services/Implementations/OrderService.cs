using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Repositories.Interfaces;

namespace Cryptocop.Software.API.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;
    private readonly IShoppingCartRepository _cartRepo;
    public OrderService(IOrderRepository repo, IShoppingCartRepository cartRepo)
    {
        _repo = repo;
        _cartRepo = cartRepo;
    }


    public Task<IEnumerable<OrderDto>> GetOrdersAsync(string email)
    {
        return _repo.GetOrdersAsync(email);
    }

    public Task CreateNewOrderAsync(string email, OrderInputModel order)
    {
        var thatNewOrder = _repo.CreateNewOrderAsync(email, order);
        _cartRepo.DeleteCartAsync(email);

        // TODO:
        // Publish a message to RabbitMQ with the routing key ‘create-order’ and include the newly created order

        return thatNewOrder;
    }
}