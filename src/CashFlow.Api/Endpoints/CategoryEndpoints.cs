using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;
using CashFlow.Application.Commands;
using CashFlow.Application.Dtos;
using CashFlow.Application.Pagination;
using CashFlow.Application.Queries;
using CashFlow.Application.Requests;
using CashFlow.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Endpoints;

public sealed class CategoryEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet().HasApiVersion(new ApiVersion(1)).ReportApiVersions().Build();

        RouteGroupBuilder group = app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(apiVersionSet);

        group.MapGet("categories", GetCategoriesAsync);
        group.MapGet("categories/{id:guid}", GetCategoryAsync);
        group.MapPost("categories", CreateCategoryAsync);
        group.MapPut("categories/{id:guid}", UpdateCategoryAsync);
    }

    private static async Task<IResult> GetCategoriesAsync(
        HttpContext context,
        ISender sender,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] CategorySortBy? sortBy = null,
        [FromQuery] string? searchQuery = null)
    {
        PagedList<CategoryDto> categories = await sender.Send(new GetCategoriesQuery(pageNumber, pageSize, sortBy, searchQuery));

        return Results.Ok(categories);
    }

    private static async Task<IResult> GetCategoryAsync(HttpContext context, ISender sender, [FromRoute] Guid id)
    {
        CategoryDto category = await sender.Send(new GetCategoryQuery(id));

        return Results.Ok(category);
    }

    private static async Task<IResult> CreateCategoryAsync(HttpContext context, ISender sender, [FromBody] CreateCategoryCommand command)
    {
        CategoryDto category = await sender.Send(command);

        return Results.Created($"categories/{category.Id}", category);
    }

    private static async Task<IResult> UpdateCategoryAsync(
        HttpContext context,
        ISender sender,
        [FromRoute] Guid id,
        [FromBody] UpdateCategoryRequest request)
    {
        UpdateCategoryCommand command = new(id, request.Name);

        await sender.Send(command);

        return Results.NoContent();
    }
}