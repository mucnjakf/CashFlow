using CashFlow.Core.Constants;
using CashFlow.Core.Entities.Abstract;
using CashFlow.Core.Exceptions;

namespace CashFlow.Core.Entities;

public sealed class Category : Entity
{
    public string Name { get; private set; }

    public IList<Transaction>? Transactions { get; private set; }

    private Category(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public static Category Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new CategoryException(Errors.Category.NameRequired);
        }

        Guid id = Guid.NewGuid();

        return new Category(id, name);
    }

    public void Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new CategoryException(Errors.Category.NameRequired);
        }

        Name = name;
        UpdatedUtc = DateTime.UtcNow;
    }
}