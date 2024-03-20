using System.Net;
using CashFlow.Core.Constants;
using CashFlow.Core.Entities.Abstract;
using CashFlow.Core.Exceptions;

namespace CashFlow.Core.Entities;

/// <summary>
/// Category entity
/// </summary>
public sealed class Category : Entity
{
    public string Name { get; private set; }

    public IList<Transaction>? Transactions { get; private set; }

    private Category(Guid id, string name) : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// Creates category entity
    /// </summary>
    /// <param name="name">Category name</param>
    /// <returns><see cref="Category"/></returns>
    /// <exception cref="CategoryException">Name is required</exception>
    public static Category Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new CategoryException(HttpStatusCode.BadRequest, Errors.Category.NameRequired);
        }

        Guid id = Guid.NewGuid();

        return new Category(id, name);
    }

    /// <summary>
    /// Updates category entity
    /// </summary>
    /// <param name="name">Category name</param>
    /// <exception cref="CategoryException">Name is required</exception>
    public void Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new CategoryException(HttpStatusCode.BadRequest, Errors.Category.NameRequired);
        }

        Name = name;
        UpdatedUtc = DateTime.UtcNow;
    }
}