using System.Net.Http.Json;
using CashFlow.Web.Commands;
using CashFlow.Web.Dtos;
using CashFlow.Web.Enums;
using CashFlow.Web.Pagination;
using CashFlow.Web.Requests;
using CashFlow.Web.Services.Abstract;

namespace CashFlow.Web.Services;

internal sealed class TransactionService(HttpClient httpClient) : ITransactionService
{
    public async Task<PagedList<TransactionDto>> GetTransactionsAsync(
        int pageNumber = 1,
        int pageSize = 10,
        TransactionType? type = null,
        TransactionSortBy? sortBy = null,
        string? searchQuery = null)
    {
        string requestUri = $"transactions?pageNumber={pageNumber}&pageSize={pageSize}";

        if (sortBy is not null)
        {
            requestUri += $"&sortBy={sortBy}";
        }

        if (type is not null)
        {
            requestUri += $"&type={type}";
        }

        if (searchQuery is not null)
        {
            requestUri += $"&searchQuery={searchQuery}";
        }

        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(requestUri);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Status code {httpResponseMessage.StatusCode}");
        }

        PagedList<TransactionDto>? transactions = await httpResponseMessage.Content.ReadFromJsonAsync<PagedList<TransactionDto>>();

        if (transactions is null)
        {
            throw new Exception("Unable to parse paged list transaction DTO");
        }

        return transactions;
    }

    public async Task<TransactionDto> GetTransactionAsync(Guid id)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"transactions/{id}");

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Status code {httpResponseMessage.StatusCode}");
        }

        TransactionDto? transaction = await httpResponseMessage.Content.ReadFromJsonAsync<TransactionDto>();

        if (transaction is null)
        {
            throw new Exception("Unable to parse transaction DTO");
        }

        return transaction;
    }

    public async Task<TransactionDto> CreateTransactionAsync(CreateTransactionCommand command)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync("transactions", command);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Status code {httpResponseMessage.StatusCode}");
        }

        TransactionDto? transaction = await httpResponseMessage.Content.ReadFromJsonAsync<TransactionDto>();

        if (transaction is null)
        {
            throw new Exception("Unable to parse transaction DTO");
        }

        return transaction;
    }

    public async Task UpdateTransactionAsync(Guid id, UpdateTransactionRequest request)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.PutAsJsonAsync($"transactions/{id}", request);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Status code {httpResponseMessage.StatusCode}");
        }
    }

    public async Task DeleteTransactionAsync(Guid id)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync($"transactions/{id}");

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Status code {httpResponseMessage.StatusCode}");
        }
    }
}