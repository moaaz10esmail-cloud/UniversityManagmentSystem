using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Library;

namespace UniversityManagementSystem.Application.Features.Library.Queries.GetBook;

public class GetBookQuery : IRequest<Result<BookDto>>
{
    public Guid BookId { get; set; }
}
