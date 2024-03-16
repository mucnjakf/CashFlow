using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;
using CashFlow.Application.Dtos;
using CashFlow.Application.Queries;
using MediatR;

namespace CashFlow.Api.Endpoints;

public sealed class TransactionEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet().HasApiVersion(new ApiVersion(1)).ReportApiVersions().Build();

        RouteGroupBuilder group = app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(apiVersionSet);

        group.MapGet("transactions", HandleGetTransactionsAsync);
    }

    private static async Task<IResult> HandleGetTransactionsAsync(HttpContext context, ISender sender)
    {
        IEnumerable<TransactionDto> transactions = await sender.Send(new GetTransactionsQuery());

        return Results.Ok(transactions);
    }
}