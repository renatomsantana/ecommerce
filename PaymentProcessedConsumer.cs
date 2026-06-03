using Contracts;
using MassTransit;

namespace NotificationService.Consumers;

public class PaymentProcessedConsumer : IConsumer<PaymentProcessed>
{
    private readonly ILogger<PaymentProcessedConsumer> _logger;

    public PaymentProcessedConsumer(ILogger<PaymentProcessedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PaymentProcessed> context)
    {
        var payment = context.Message;

        if (payment.Success)
        {
            _logger.LogInformation("EMAIL ENVIADO para {Email}: Seu pedido {OrderId} foi confirmado!",
                payment.CustomerEmail, payment.OrderId);
        }
        else
        {
            _logger.LogInformation("EMAIL ENVIADO para {Email}: Pagamento recusado para o pedido {OrderId}.",
                payment.CustomerEmail, payment.OrderId);
        }

        await Task.CompletedTask;
    }
}