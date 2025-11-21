using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Finance;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Finance.Commands.GenerateInvoice;

public class GenerateInvoiceCommand : IRequest<Result<InvoiceDto>>
{
    public Guid StudentId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public List<InvoiceItemRequest> Items { get; set; } = new();
    public string? Notes { get; set; }
}

public class InvoiceItemRequest
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int Quantity { get; set; } = 1;
}
