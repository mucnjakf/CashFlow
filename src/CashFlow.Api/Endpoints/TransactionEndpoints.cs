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

        group.MapGet("transactions", GetTransactionsAsync);
        group.MapGet("transactions/{id:guid}", GetTransactionAsync);
        group.MapPost("transactions", CreateTransactionAsync);
        group.MapPut("transactions/{id:guid}", UpdateTransactionAsync);
        group.MapDelete("transactions/{id:guid}", DeleteTransactionAsync);
    }

    private static async Task<IResult> GetTransactionsAsync(HttpContext context, ISender sender)
    {
        IEnumerable<TransactionDto> transactions = await sender.Send(new GetTransactionsQuery());

        return Results.Ok(transactions);
    }

    private static async Task<IResult> GetTransactionAsync(HttpContext context, ISender sender, [FromRoute] Guid id)
    {
        TransactionDto transaction = await sender.Send(new GetTransactionQuery(id));

        return Results.Ok(transaction);
    }

    private static async Task<IResult> CreateTransactionAsync(HttpContext context, ISender sender, [FromBody] CreateTransactionCommand command)
    {
        TransactionDto transaction = await sender.Send(command);

        return Results.Created($"transactions/{transaction.Id}", transaction);
    }

    private static async Task<IResult> UpdateTransactionAsync(
        HttpContext context,
        ISender sender,
        [FromRoute] Guid id,
        [FromBody] UpdateTransactionRequest request)
    {
        UpdateTransactionCommand command = new(id, request.DateTimeUtc, request.Description, request.CategoryId);

        await sender.Send(command);

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteTransactionAsync(HttpContext context, ISender sender, [FromRoute] Guid id)
    {
        await sender.Send(new DeleteTransactionCommand(id));

        return Results.NoContent();
    }
}