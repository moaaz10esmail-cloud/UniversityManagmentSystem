using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.Features.Library.Commands.BorrowBook;
using UniversityManagementSystem.Application.Features.Library.Commands.ReturnBook;
using UniversityManagementSystem.Application.Features.Library.Queries.SearchBooks;
using UniversityManagementSystem.Application.Features.Library.Queries.GetBook;
using UniversityManagementSystem.Application.Features.Shared.Queries;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LibraryController : BaseApiController
{
    [HttpPost("books/borrow")]
    public async Task<IActionResult> BorrowBook(BorrowBookCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("books/return")]
    public async Task<IActionResult> ReturnBook(ReturnBookCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet("books/search")]
    public async Task<IActionResult> SearchBooks([FromQuery] SearchBooksQuery query)
    {
        var result = await Mediator.Send(query);
        return HandleResult(result);
    }

    [HttpGet("books/{bookId}")]
    public async Task<IActionResult> GetBook(Guid bookId)
    {
        var result = await Mediator.Send(new GetBookQuery { BookId = bookId });
        return HandleResult(result);
    }

    [HttpGet("students/{studentId}/loans")]
    public async Task<IActionResult> GetStudentLoans(Guid studentId)
    {
        var result = await Mediator.Send(new GetStudentLoansQuery { StudentId = studentId });
        return HandleResult(result);
    }

    [HttpPost("books")]
    [Authorize(Roles = "Admin,Librarian")]
    public async Task<IActionResult> AddBook(AddBookCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }
}
