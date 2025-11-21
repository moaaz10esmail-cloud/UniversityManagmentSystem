using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Entities.Library;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Library.Commands.BorrowBook;

public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public BorrowBookCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .FirstOrDefaultAsync(b => b.Id == request.BookId && b.IsActive, cancellationToken);

        if (book == null)
        {
            return Result.Failure("Book not found or inactive");
        }

        if (book.AvailableCopies <= 0)
        {
            return Result.Failure("No copies available for borrowing");
        }

        // Check if student already has this book
        var existingLoan = await _context.BookLoans
            .AnyAsync(bl => bl.BookId == request.BookId && 
                           bl.StudentId == request.StudentId && 
                           bl.Status == LoanStatus.Active, 
                      cancellationToken);

        if (existingLoan)
        {
            return Result.Failure("Student already has an active loan for this book");
        }

        // Check student's active loans limit (e.g., max 5 books)
        var activeLoansCount = await _context.BookLoans
            .CountAsync(bl => bl.StudentId == request.StudentId && bl.Status == LoanStatus.Active, 
                       cancellationToken);

        if (activeLoansCount >= 5)
        {
            return Result.Failure("Student has reached maximum number of active loans (5)");
        }

        var loan = new BookLoan
        {
            BookId = request.BookId,
            StudentId = request.StudentId,
            LoanDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(request.LoanDurationDays),
            Status = LoanStatus.Active
        };

        _context.BookLoans.Add(loan);
        book.AvailableCopies--;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success($"Book '{book.Title}' borrowed successfully. Due date: {loan.DueDate:d}");
    }
}
