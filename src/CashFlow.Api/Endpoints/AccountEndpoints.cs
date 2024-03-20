using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;
using CashFlow.Api.Handlers;
using CashFlow.Application.Commands;
using CashFlow.Application.Dtos;
using CashFlow.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Endpoints;

/// <summary>
/// Account endpoints
/// </summary>
public sealed class AccountEndpoints : ICarterModule
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
            .MapGet("account", GetAccountAsync)
            .WithSummary("Get account")
            .Produces(200, typeof(AccountDto))
            .Produces(400, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(500, typeof(GlobalExceptionHandler.ErrorResponseDto));

        group
            .MapPut("account", UpdateAccountAsync)
            .WithSummary("Update account")
            .Produces(204)
            .Produces(400, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(401, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(500, typeof(GlobalExceptionHandler.ErrorResponseDto));
    }

    /// <summary>
    /// Get account endpoint handler
    /// </summary>
    /// <param name="context"><see cref="HttpContext"/></param>
    /// <param name="sender"><see cref="ISender"/></param>
    /// <returns><see cref="AccountDto"/> with 200 OK status code</returns>
    private static async Task<IResult> GetAccountAsync(HttpContext context, ISender sender)
    {
        AccountDto account = await sender.Send(new GetAccountQuery());

        return Results.Ok(account);
    }

    /// <summary>
    /// Update account endpoint handler
    /// </summary>
    /// <param name="context"><see cref="HttpContext"/></param>
    /// <param name="sender"><see cref="ISender"/></param>
    /// <param name="command"><see cref="UpdateAccountCommand"/></param>
    /// <returns>204 No content status code</returns>
    private static async Task<IResult> UpdateAccountAsync(HttpContext context, ISender sender, [FromBody] UpdateAccountCommand command)
    {
        await sender.Send(command);

        return Results.NoContent();
    }
}