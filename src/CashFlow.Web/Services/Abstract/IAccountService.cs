using CashFlow.Web.Commands;
using CashFlow.Web.Dtos;

namespace CashFlow.Web.Services.Abstract;

internal interface IAccountService
{
    Task<AccountDto> GetAccountAsync();

    Task UpdateAccountAsync(UpdateAccountCommand command);
}