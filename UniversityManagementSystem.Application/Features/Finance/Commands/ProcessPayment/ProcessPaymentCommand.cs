using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Finance.Commands.ProcessPayment;

public class ProcessPaymentCommand : IRequest<Result>
{
    public Guid InvoiceId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public string? ReferenceNumber { get; set; }
    public string? Notes { get; set; }
}
