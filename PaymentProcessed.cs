namespace Contracts;

public record PaymentProcessed
{
    public Guid OrderId { get; init; }
    public string CustomerEmail { get; init; } = string.Empty;
    public bool Success { get; init; }
    public DateTime ProcessedAt { get; init; }
}