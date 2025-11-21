using Microsoft.AspNetCore.Http;

namespace UniversityManagementSystem.API.Extensions;

public static class HttpResponseExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, int currentPage, int pageSize, int totalCount, int totalPages)
    {
        var paginationHeader = new
        {
            currentPage,
            pageSize,
            totalCount,
            totalPages,
            hasPrevious = currentPage > 1,
            hasNext = currentPage < totalPages
        };

        response.Headers.Append("X-Pagination", System.Text.Json.JsonSerializer.Serialize(paginationHeader));
    }
}

