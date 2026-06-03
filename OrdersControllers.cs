using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace OrdersApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrdersController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var orderId = Guid.NewGuid();

        await _publishEndpoint.Publish<OrderCreated>(new
        {
            OrderId = orderId,
            request.CustomerEmail,
            request.TotalAmount,
            CreatedAt = DateTime.UtcNow
        });

        return Accepted(new { orderId, message = "Pedido recebido, processando pagamento." });
    }
}

public record CreateOrderRequest(string CustomerEmail, decimal TotalAmount);