using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;
using CashFlow.Application.Commands;
using CashFlow.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Endpoints;

public sealed class CategoryEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet().HasApiVersion(new ApiVersion(1)).ReportApiVersions().Build();

        RouteGroupBuilder group = app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(apiVersionSet);

        group.MapPost("categories", CreateCategoryAsync);
    }

    private static async Task<IResult> CreateCategoryAsync(HttpContext context, ISender sender, [FromBody] CreateCategoryCommand command)
    {
        CategoryDto category = await sender.Send(command);

        return Results.Created($"categories/{category.Id}", category);
    }
}