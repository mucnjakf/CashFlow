using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;
using CashFlow.Api.Handlers;
using CashFlow.Application.Commands;
using CashFlow.Application.Dtos;
using CashFlow.Application.Pagination;
using CashFlow.Application.Queries;
using CashFlow.Application.Requests;
using CashFlow.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Endpoints;

/// <summary>
/// Transaction endpoints
/// </summary>
public sealed class TransactionEndpoints : ICarterModule
{
    /// <summary>
    /// Builds and registers routes
    /// </summary>
    /// <param name="app"><see cref="IEndpointRouteBuilder"/></param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app
            .NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        RouteGroupBuilder group = app
            .MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(apiVersionSet)
            .WithOpenApi();

        group
            .MapGet("transactions", GetTransactionsAsync)
            .WithSummary("Get transactions")
            .Produces(200, typeof(PagedList<TransactionDto>))
            .Produces(500, typeof(GlobalExceptionHandler.ErrorResponseDto));

        group
            .MapGet("transactions/{id:guid}", GetTransactionAsync)
            .WithSummary("Get transaction")
            .Produces(200, typeof(TransactionDto))
            .Produces(401, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(500, typeof(GlobalExceptionHandler.ErrorResponseDto));

        group
            .MapPost("transactions", CreateTransactionAsync)
            .WithSummary("Create transaction")
            .Produces(201, typeof(CategoryDto))
            .Produces(400, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(401, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(500, typeof(GlobalExceptionHandler.ErrorResponseDto));

        group
            .MapPut("transactions/{id:guid}", UpdateTransactionAsync)
            .WithSummary("Update transaction")
            .Produces(204)
            .Produces(400, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(401, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(500, typeof(GlobalExceptionHandler.ErrorResponseDto));

        group
            .MapDelete("transactions/{id:guid}", DeleteTransactionAsync).WithSummary("Delete category")
            .Produces(204)
            .Produces(400, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(401, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(500, typeof(GlobalExceptionHandler.ErrorResponseDto));
    }

    /// <summary>
    /// Get transactions endpoint handler
    /// </summary>
    /// <param name="context"><see cref="HttpContext"/></param>
    /// <param name="sender"><see cref="ISender"/></param>
    /// <param name="pageNumber">Pagination page number</param>
    /// <param name="pageSize">Pagination page size</param>
    /// <param name="type"><see cref="TransactionType"/></param>
    /// <param name="sortBy"><see cref="TransactionSortBy"/></param>
    /// <param name="searchQuery">Search query for filtering</param>
    /// <returns><see cref="PagedList{T}"/> with 200 OK status code</returns>
    private static async Task<IResult> GetTransactionsAsync(
        HttpContext context,
        ISender sender,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] TransactionType? type = null,
        [FromQuery] TransactionSortBy? sortBy = null,
        [FromQuery] string? searchQuery = null)
    {
        PagedList<TransactionDto> transactions = await sender.Send(new GetTransactionsQuery(pageNumber, pageSize, type, sortBy, searchQuery));

        return Results.Ok(transactions);
    }

    /// <summary>
    /// Get transaction endpoint handler
    /// </summary>
    /// <param name="context"><see cref="HttpContext"/></param>
    /// <param name="sender"><see cref="ISender"/></param>
    /// <param name="id">Transaction ID</param>
    /// <returns><see cref="TransactionDto"/> with 200 OK status code</returns>
    private static async Task<IResult> GetTransactionAsync(HttpContext context, ISender sender, [FromRoute] Guid id)
    {
        TransactionDto transaction = await sender.Send(new GetTransactionQuery(id));

        return Results.Ok(transaction);
    }

    /// <summary>
    /// Create transaction endpoint handler
    /// </summary>
    /// <param name="context"><see cref="HttpContext"/></param>
    /// <param name="sender"><see cref="ISender"/></param>
    /// <param name="command"><see cref="CreateTransactionCommand"/></param>
    /// <returns><see cref="TransactionDto"/> with 201 Created status code</returns>
    private static async Task<IResult> CreateTransactionAsync(HttpContext context, ISender sender, [FromBody] CreateTransactionCommand command)
    {
        TransactionDto transaction = await sender.Send(command);

        return Results.Created($"transactions/{transaction.Id}", transaction);
    }

    /// <summary>
    /// Update transaction endpoint handler
    /// </summary>
    /// <param name="context"><see cref="HttpContext"/></param>
    /// <param name="sender"><see cref="ISender"/></param>
    /// <param name="id">Transaction ID</param>
    /// <param name="request"><see cref="UpdateTransactionRequest"/></param>
    /// <returns>204 No content status code</returns>
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

    /// <summary>
    /// Delete transaction endpoint handler
    /// </summary>
    /// <param name="context"><see cref="HttpContext"/></param>
    /// <param name="sender"><see cref="ISender"/></param>
    /// <param name="id">Transaction ID</param>
    /// <returns>204 No content status code</returns>
    private static async Task<IResult> DeleteTransactionAsync(HttpContext context, ISender sender, [FromRoute] Guid id)
    {
        await sender.Send(new DeleteTransactionCommand(id));

        return Results.NoContent();
    }
}