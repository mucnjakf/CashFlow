using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;
using CashFlow.Application.Commands;
using CashFlow.Application.Dtos;
using CashFlow.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Endpoints;

public sealed class AccountEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet().HasApiVersion(new ApiVersion(1)).ReportApiVersions().Build();

        RouteGroupBuilder group = app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(apiVersionSet);

        group.MapGet("account", GetAccountAsync);
        group.MapPut("account", UpdateAccountAsync);
    }

    private static async Task<IResult> GetAccountAsync(HttpContext context, ISender sender)
    {
        AccountDto account = await sender.Send(new GetAccountQuery());

        return Results.Ok(account);
    }

    private static async Task<IResult> UpdateAccountAsync(HttpContext context, ISender sender, [FromBody] UpdateAccountCommand command)
    {
        await sender.Send(command);

        return Results.NoContent();
    }
}