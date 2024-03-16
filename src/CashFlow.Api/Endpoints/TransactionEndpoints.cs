using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;
using CashFlow.Application.Commands;
using CashFlow.Application.Dtos;
using CashFlow.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Endpoints;

public sealed class TransactionEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet().HasApiVersion(new ApiVersion(1)).ReportApiVersions().Build();

        RouteGroupBuilder group = app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(apiVersionSet);

        group.MapGet("transactions", HandleGetTransactionsAsync);
        group.MapPost("transactions", HandleCreateTransactionAsync);
    }

    private static async Task<IResult> HandleGetTransactionsAsync(HttpContext context, ISender sender)
    {
        IEnumerable<TransactionDto> transactions = await sender.Send(new GetTransactionsQuery());

        return Results.Ok(transactions);
    }
    
    private static async Task<IResult> HandleCreateTransactionAsync(HttpContext context, ISender sender, [FromBody] CreateTransactionCommand command)
    {
        TransactionDto transaction = await sender.Send(command);

        return Results.Ok(transaction);

        // TODO: implement created at
    }
}