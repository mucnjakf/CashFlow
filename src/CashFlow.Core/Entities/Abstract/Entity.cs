namespace CashFlow.Core.Entities.Abstract;

public abstract class Entity(Guid id)
{
    public Guid Id { get; private set; } = id;

    public DateTime CreatedUtc { get; private set; } = DateTime.UtcNow;

    public DateTime UpdatedUtc { get; private set; } = DateTime.UtcNow;
}