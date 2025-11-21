using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Library.Commands.ReturnBook;

public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public ReturnBookCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        var loan = await _context.BookLoans
            .Include(bl => bl.Book)
            .FirstOrDefaultAsync(bl => bl.Id == request.BookLoanId, cancellationToken);

        if (loan == null)
        {
            return Result.Failure("Book loan not found");
        }

        if (loan.Status == LoanStatus.Returned)
        {
            return Result.Failure("Book has already been returned");
        }

        loan.ReturnDate = DateTime.UtcNow;
        loan.Status = LoanStatus.Returned;

        // Calculate fine if overdue
        if (loan.ReturnDate > loan.DueDate)
        {
            var daysOverdue = (loan.ReturnDate.Value - loan.DueDate).Days;
            var finePerDay = 1.00m; // $1 per day
            loan.FineAmount = daysOverdue * finePerDay;
        }

        // Increase available copies
        loan.Book.AvailableCopies++;

        await _context.SaveChangesAsync(cancellationToken);

        var message = loan.FineAmount.HasValue && loan.FineAmount > 0
            ? $"Book returned successfully. Fine amount: {loan.FineAmount:C}"
            : "Book returned successfully";

        return Result.Success(message);
    }
}
