using CashFlow.Web.Commands;
using CashFlow.Web.Dtos;
using CashFlow.Web.Enums;
using CashFlow.Web.Pagination;
using CashFlow.Web.Requests;

namespace CashFlow.Web.Services.Abstract;

internal interface ITransactionService
{
    Task<PagedList<TransactionDto>> GetTransactionsAsync(
        int pageNumber = 1,
        int pageSize = 10,
        TransactionType? type = null,
        TransactionSortBy? sortBy = null,
        string? searchQuery = null
    );

    Task<TransactionDto> GetTransactionAsync(Guid id);

    Task<TransactionDto> CreateTransactionAsync(CreateTransactionCommand command);

    Task UpdateTransactionAsync(Guid id, UpdateTransactionRequest request);

    Task DeleteTransactionAsync(Guid id);
}