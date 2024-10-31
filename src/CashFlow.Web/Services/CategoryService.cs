using System.Net.Http.Json;
using CashFlow.Web.Commands;
using CashFlow.Web.Dtos;
using CashFlow.Web.Enums;
using CashFlow.Web.Pagination;
using CashFlow.Web.Requests;
using CashFlow.Web.Services.Abstract;

namespace CashFlow.Web.Services;

internal sealed class CategoryService(HttpClient httpClient) : ICategoryService
{
    public async Task<PagedList<CategoryDto>> GetCategoriesAsync(
        int pageNumber = 1,
        int pageSize = 10,
        CategorySortBy? sortBy = null,
        string? searchQuery = null)
    {
        string requestUri = $"categories?pageNumber={pageNumber}&pageSize={pageSize}";

        if (sortBy is not null)
        {
            requestUri += $"&sortBy={sortBy}";
        }

        if (searchQuery is not null)
        {
            requestUri += $"&searchQuery={searchQuery}";
        }

        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(requestUri);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Status code {httpResponseMessage.StatusCode}");
        }

        PagedList<CategoryDto>? categories = await httpResponseMessage.Content.ReadFromJsonAsync<PagedList<CategoryDto>>();

        if (categories is null)
        {
            throw new Exception("Unable to parse paged list category DTO");
        }

        return categories;
    }

    public async Task<CategoryDto> GetCategoryAsync(Guid id)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"categories/{id}");

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Status code {httpResponseMessage.StatusCode}");
        }

        CategoryDto? category = await httpResponseMessage.Content.ReadFromJsonAsync<CategoryDto>();

        if (category is null)
        {
            throw new Exception("Unable to parse category DTO");
        }

        return category;
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryCommand command)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync("categories", command);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Status code {httpResponseMessage.StatusCode}");
        }

        CategoryDto? category = await httpResponseMessage.Content.ReadFromJsonAsync<CategoryDto>();

        if (category is null)
        {
            throw new Exception("Unable to parse category DTO");
        }

        return category;
    }

    public async Task UpdateCategoryAsync(Guid id, UpdateCategoryRequest request)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.PutAsJsonAsync($"categories/{id}", request);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Status code {httpResponseMessage.StatusCode}");
        }
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync($"categories/{id}");

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Status code {httpResponseMessage.StatusCode}");
        }
    }
}