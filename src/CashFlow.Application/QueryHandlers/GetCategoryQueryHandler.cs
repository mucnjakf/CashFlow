using System.Net;
using CashFlow.Application.Context;
using CashFlow.Application.Dtos;
using CashFlow.Application.Mappers;
using CashFlow.Application.Queries;
using CashFlow.Core.Constants;
using CashFlow.Core.Entities;
using CashFlow.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.QueryHandlers;

/// <summary>
/// Get category query handler
/// </summary>
/// <param name="dbContext"><see cref="IApplicationDbContext"/></param>
internal sealed class GetCategoryQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetCategoryQuery, CategoryDto>
{
    /// <summary>
    /// Handles getting category
    /// </summary>
    /// <param name="query"><see cref="GetCategoryQuery"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="CategoryDto"/></returns>
    /// <exception cref="CategoryException">Category not found</exception>
    public async Task<CategoryDto> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
    {
        Category? category = await dbContext.Categories
            .Include(x => x.Transactions)!
            .ThenInclude(x => x.Account)
            .SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (category is null)
        {
            throw new CategoryException(HttpStatusCode.NotFound, Errors.Category.CategoryNotFound);
        }

        return category.ToCategoryDto();
    }
}