using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;
using CashFlow.Application.Commands;
using CashFlow.Application.Dtos;
using MediatR;

namespace CashFlow.Api.Endpoints;

public sealed class TestEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet().HasApiVersion(new ApiVersion(1)).ReportApiVersions().Build();

        RouteGroupBuilder group = app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(apiVersionSet);

        group.MapGet("test", HandlerAsync);
    }

    private static async Task<IResult> HandlerAsync(HttpContext context, ISender sender)
    {
        TestDto result = await sender.Send(new CreateTestCommand("Test"));

        return Results.Ok(result);
    }
}