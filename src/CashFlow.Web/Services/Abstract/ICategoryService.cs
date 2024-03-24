using CashFlow.Web.Commands;
using CashFlow.Web.Dtos;
using CashFlow.Web.Enums;
using CashFlow.Web.Pagination;
using CashFlow.Web.Requests;

namespace CashFlow.Web.Services.Abstract;

internal interface ICategoryService
{
    Task<PagedList<CategoryDto>> GetCategoriesAsync(
        int pageNumber = 1,
        int pageSize = 10,
        CategorySortBy? sortBy = null,
        string? searchQuery = null);

    Task<CategoryDto> GetCategoryAsync(Guid id);

    Task<CategoryDto> CreateCategoryAsync(CreateCategoryCommand command);

    Task UpdateCategoryAsync(Guid id, UpdateCategoryRequest request);

    Task DeleteCategoryAsync(Guid id);
}