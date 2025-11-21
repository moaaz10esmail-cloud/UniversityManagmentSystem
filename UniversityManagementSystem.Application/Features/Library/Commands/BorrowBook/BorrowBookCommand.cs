using MediatR;
using UniversityManagementSystem.Application.Common;

namespace UniversityManagementSystem.Application.Features.Library.Commands.BorrowBook;

public class BorrowBookCommand : IRequest<Result>
{
    public Guid BookId { get; set; }
    public Guid StudentId { get; set; }
    public int LoanDurationDays { get; set; } = 14; // Default 2 weeks
}
