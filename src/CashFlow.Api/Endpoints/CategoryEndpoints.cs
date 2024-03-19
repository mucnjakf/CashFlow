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

public sealed class CategoryEndpoints : ICarterModule
{
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
            .MapGet("categories", GetCategoriesAsync)
            .WithSummary("Get categories")
            .Produces(200, typeof(PagedList<CategoryDto>))
            .Produces(500, typeof(GlobalExceptionHandler.ErrorResponseDto));

        group
            .MapGet("categories/{id:guid}", GetCategoryAsync)
            .WithSummary("Get category")
            .Produces(200, typeof(CategoryDto))
            .Produces(401, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(500, typeof(GlobalExceptionHandler.ErrorResponseDto));

        group
            .MapPost("categories", CreateCategoryAsync)
            .WithSummary("Create category")
            .Produces(201, typeof(CategoryDto))
            .Produces(400, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(500, typeof(GlobalExceptionHandler.ErrorResponseDto));

        group
            .MapPut("categories/{id:guid}", UpdateCategoryAsync)
            .WithSummary("Update category")
            .Produces(204)
            .Produces(400, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(401, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(500, typeof(GlobalExceptionHandler.ErrorResponseDto));

        group
            .MapDelete("categories/{id:guid}", DeleteCategoryAsync)
            .WithSummary("Delete category")
            .Produces(204)
            .Produces(400, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(401, typeof(GlobalExceptionHandler.ErrorResponseDto))
            .Produces(500, typeof(GlobalExceptionHandler.ErrorResponseDto));
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

    private static async Task<IResult> DeleteCategoryAsync(HttpContext context, ISender sender, [FromRoute] Guid id)
    {
        await sender.Send(new DeleteCategoryCommand(id));

        return Results.NoContent();
    }
}