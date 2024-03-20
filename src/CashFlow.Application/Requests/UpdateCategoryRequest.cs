namespace CashFlow.Application.Requests;

/// <summary>
/// Update category request
/// </summary>
/// <param name="Name">Category name</param>
public sealed record UpdateCategoryRequest(string Name);