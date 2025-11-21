using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Library;

namespace UniversityManagementSystem.Application.Features.Library.Queries.SearchBooks;

public class SearchBooksQuery : IRequest<Result<BookSearchResultDto>>
{
    public string? SearchTerm { get; set; }
    public string? Category { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
