using Microsoft.Extensions.Options;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Helpers;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Repositories.Interfaces;

namespace Cryptocop.Software.API.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;
    private readonly IShoppingCartRepository _cartRepo;
    private readonly IQueueService _queueService;
    private readonly string _createOrderRoutingKey;
    public OrderService(
        IOrderRepository repo,
        IShoppingCartRepository cartRepo,
        IQueueService queueService,
        IOptions<RabbitMqSettings> rabbitMqOptions)
    {
        _repo = repo;
        _cartRepo = cartRepo;
        _queueService = queueService;
        var routingKey = rabbitMqOptions.Value.RoutingKey;
        _createOrderRoutingKey = string.IsNullOrWhiteSpace(routingKey) ? "create-order" : routingKey;
    }


    public Task<IEnumerable<OrderDto>> GetOrdersAsync(string email)
    {
        return _repo.GetOrdersAsync(email);
    }

    public async Task CreateNewOrderAsync(string email, OrderInputModel order)
    {
        var createdOrder = await _repo.CreateNewOrderAsync(email, order);
        await _cartRepo.DeleteCartAsync(email);
        await _queueService.PublishMessageAsync(_createOrderRoutingKey, createdOrder);
    }
}