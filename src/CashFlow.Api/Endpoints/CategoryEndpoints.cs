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
/// Category endpoints
/// </summary>
public sealed class CategoryEndpoints : ICarterModule
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

    /// <summary>
    /// Get categories endpoint handler
    /// </summary>
    /// <param name="context"><see cref="HttpContext"/></param>
    /// <param name="sender"><see cref="ISender"/></param>
    /// <param name="pageNumber">Pagination page number</param>
    /// <param name="pageSize">Pagination page size</param>
    /// <param name="sortBy"><see cref="CategorySortBy"/></param>
    /// <param name="searchQuery">Search query for filtering</param>
    /// <returns><see cref="PagedList{T}"/> with 200 OK status code</returns>
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

    /// <summary>
    /// Get category endpoint handler
    /// </summary>
    /// <param name="context"><see cref="HttpContext"/></param>
    /// <param name="sender"><see cref="ISender"/></param>
    /// <param name="id">Category ID</param>
    /// <returns><see cref="CategoryDto"/> with 200 OK status code</returns>
    private static async Task<IResult> GetCategoryAsync(HttpContext context, ISender sender, [FromRoute] Guid id)
    {
        CategoryDto category = await sender.Send(new GetCategoryQuery(id));

        return Results.Ok(category);
    }

    /// <summary>
    /// Create category endpoint handler
    /// </summary>
    /// <param name="context"><see cref="HttpContext"/></param>
    /// <param name="sender"><see cref="ISender"/></param>
    /// <param name="command"><see cref="CreateCategoryCommand"/></param>
    /// <returns><see cref="CategoryDto"/> with 201 Created status code</returns>
    private static async Task<IResult> CreateCategoryAsync(HttpContext context, ISender sender, [FromBody] CreateCategoryCommand command)
    {
        CategoryDto category = await sender.Send(command);

        return Results.Created($"categories/{category.Id}", category);
    }

    /// <summary>
    /// Update category endpoint handler
    /// </summary>
    /// <param name="context"><see cref="HttpContext"/></param>
    /// <param name="sender"><see cref="ISender"/></param>
    /// <param name="id">Category ID</param>
    /// <param name="request"><see cref="UpdateCategoryRequest"/></param>
    /// <returns>204 No content status code</returns>
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

    /// <summary>
    /// Delete category endpoint handler
    /// </summary>
    /// <param name="context"><see cref="HttpContext"/></param>
    /// <param name="sender"><see cref="ISender"/></param>
    /// <param name="id">Category ID</param>
    /// <returns>204 No content status code</returns>
    private static async Task<IResult> DeleteCategoryAsync(HttpContext context, ISender sender, [FromRoute] Guid id)
    {
        await sender.Send(new DeleteCategoryCommand(id));

        return Results.NoContent();
    }
}