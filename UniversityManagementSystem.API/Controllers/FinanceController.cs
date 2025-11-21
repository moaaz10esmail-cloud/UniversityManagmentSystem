using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.Features.Finance.Commands.GenerateInvoice;
using UniversityManagementSystem.Application.Features.Finance.Commands.ProcessPayment;
using UniversityManagementSystem.Application.Features.Finance.Queries.GetFinancialSummary;
using UniversityManagementSystem.Application.Features.Finance.Queries.GetInvoice;
using UniversityManagementSystem.Application.Features.Shared.Queries;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Finance")]
public class FinanceController : BaseApiController
{
    [HttpPost("invoices/generate")]
    public async Task<IActionResult> GenerateInvoice(GenerateInvoiceCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("payments/process")]
    public async Task<IActionResult> ProcessPayment(ProcessPaymentCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet("students/{studentId}/financial-summary")]
    public async Task<IActionResult> GetFinancialSummary(Guid studentId)
    {
        var result = await Mediator.Send(new GetFinancialSummaryQuery { StudentId = studentId });
        return HandleResult(result);
    }

    [HttpGet("invoices/{invoiceId}")]
    public async Task<IActionResult> GetInvoice(Guid invoiceId)
    {
        var result = await Mediator.Send(new GetInvoiceQuery { InvoiceId = invoiceId });
        return HandleResult(result);
    }

    [HttpGet("students/{studentId}/invoices")]
    public async Task<IActionResult> GetStudentInvoices(Guid studentId)
    {
        var result = await Mediator.Send(new GetStudentInvoicesQuery { StudentId = studentId });
        return HandleResult(result);
    }
}
