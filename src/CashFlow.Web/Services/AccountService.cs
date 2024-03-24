using System.Net.Http.Json;
using CashFlow.Web.Commands;
using CashFlow.Web.Dtos;
using CashFlow.Web.Services.Abstract;

namespace CashFlow.Web.Services;

internal sealed class AccountService(HttpClient httpClient) : IAccountService
{
    public async Task<AccountDto> GetAccountAsync()
    {
        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("account");

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Status code {httpResponseMessage.StatusCode}");
        }

        AccountDto? account = await httpResponseMessage.Content.ReadFromJsonAsync<AccountDto>();

        if (account is null)
        {
            throw new Exception("Unable to parse account DTO");
        }

        return account;
    }

    public async Task UpdateAccountAsync(UpdateAccountCommand command)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.PutAsJsonAsync("account", command);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Status code {httpResponseMessage.StatusCode}");
        }
    }
}