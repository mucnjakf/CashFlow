using Carter;
using CashFlow.Application.Commands;
using CashFlow.Application.Dtos;
using MediatR;

namespace CashFlow.Api.Endpoints;

public sealed class TestEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/test", HandlerAsync);
    }

    private static async Task<IResult> HandlerAsync(HttpContext context, ISender sender)
    {
        TestDto result = await sender.Send(new CreateTestCommand("Test"));

        return Results.Ok(result);
    }
}