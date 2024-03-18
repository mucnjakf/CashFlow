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

internal sealed class GetCategoryQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetCategoryQuery, CategoryDto>
{
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