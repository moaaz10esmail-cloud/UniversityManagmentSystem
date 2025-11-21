using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Library;
using UniversityManagementSystem.Application.Interfaces;

namespace UniversityManagementSystem.Application.Features.Library.Queries.GetBook;

public class GetBookQueryHandler : IRequestHandler<GetBookQuery, Result<BookDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBookQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<BookDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .FirstOrDefaultAsync(b => b.Id == request.BookId, cancellationToken);

        if (book == null)
        {
            return Result<BookDto>.Failure("Book not found");
        }

        var bookDto = _mapper.Map<BookDto>(book);
        return Result<BookDto>.Success(bookDto);
    }
}
