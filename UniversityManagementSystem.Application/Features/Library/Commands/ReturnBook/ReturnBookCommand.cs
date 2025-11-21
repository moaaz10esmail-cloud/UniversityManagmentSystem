using MediatR;
using UniversityManagementSystem.Application.Common;

namespace UniversityManagementSystem.Application.Features.Library.Commands.ReturnBook;

public class ReturnBookCommand : IRequest<Result>
{
    public Guid BookLoanId { get; set; }
}
