namespace CashFlow.Application.Dtos;

/// <summary>
/// Transaction category DTO
/// </summary>
/// <param name="Id">Category ID</param>
/// <param name="Name">Category name</param>
public sealed record TransactionCategoryDto(Guid Id, string Name);