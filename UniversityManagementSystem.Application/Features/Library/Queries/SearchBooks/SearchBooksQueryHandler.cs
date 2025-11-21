using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Library;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Library.Queries.SearchBooks;

public class SearchBooksQueryHandler : IRequestHandler<SearchBooksQuery, Result<BookSearchResultDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SearchBooksQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<BookSearchResultDto>> Handle(SearchBooksQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Books.Where(b => b.IsActive);

        // Apply search term filter
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.ToLower();
            query = query.Where(b => 
                b.Title.ToLower().Contains(searchTerm) ||
                b.Author.ToLower().Contains(searchTerm) ||
                b.ISBN.Contains(searchTerm));
        }

        // Apply category filter
        if (!string.IsNullOrWhiteSpace(request.Category) && Enum.TryParse<BookCategory>(request.Category, out var category))
        {
            query = query.Where(b => b.Category == category);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var books = await query
            .OrderBy(b => b.Title)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var result = new BookSearchResultDto
        {
            Books = _mapper.Map<List<BookDto>>(books),
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        return Result<BookSearchResultDto>.Success(result);
    }
}
