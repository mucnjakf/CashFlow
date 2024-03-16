using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;
using CashFlow.Application.Commands;
using CashFlow.Application.Dtos;
using CashFlow.Application.Queries;
using CashFlow.Application.Requests;
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
        group.MapGet("transactions/{id:guid}", HandleGetTransactionAsync);
        group.MapPost("transactions", HandleCreateTransactionAsync);
        group.MapPut("transactions/{id:guid}", HandleUpdateTransactionAsync);
    }

    private static async Task<IResult> HandleGetTransactionsAsync(HttpContext context, ISender sender)
    {
        IEnumerable<TransactionDto> transactions = await sender.Send(new GetTransactionsQuery());

        return Results.Ok(transactions);
    }

    private static async Task<IResult> HandleGetTransactionAsync(HttpContext context, ISender sender, [FromRoute] Guid id)
    {
        TransactionDto transaction = await sender.Send(new GetTransactionQuery(id));

        return Results.Ok(transaction);
    }

    private static async Task<IResult> HandleCreateTransactionAsync(HttpContext context, ISender sender, [FromBody] CreateTransactionCommand command)
    {
        TransactionDto transaction = await sender.Send(command);

        return Results.Created($"transactions/{transaction.Id}", transaction);
    }

    private static async Task<IResult> HandleUpdateTransactionAsync(
        HttpContext context,
        ISender sender,
        [FromRoute] Guid id,
        [FromBody] UpdateTransactionRequest request)
    {
        UpdateTransactionCommand command = new(id, request.DateTimeUtc, request.Description, request.CategoryId);

        await sender.Send(command);

        return Results.NoContent();
    }
}