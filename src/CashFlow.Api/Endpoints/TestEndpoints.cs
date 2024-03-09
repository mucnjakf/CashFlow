using Carter;

namespace CashFlow.Api.Endpoints;

public sealed class TestEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/test", HandlerAsync);
    }

    private static async Task<IResult> HandlerAsync()
    {
        await Task.Delay(1000);

        return Results.Ok("TEST");
    }
}