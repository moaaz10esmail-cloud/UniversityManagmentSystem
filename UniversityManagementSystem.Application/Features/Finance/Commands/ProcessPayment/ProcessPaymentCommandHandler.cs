using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Entities.Finance;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Finance.Commands.ProcessPayment;

public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public ProcessPaymentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _context.Invoices
            .FirstOrDefaultAsync(i => i.Id == request.InvoiceId, cancellationToken);

        if (invoice == null)
        {
            return Result.Failure("Invoice not found");
        }

        if (invoice.Status == InvoiceStatus.Paid)
        {
            return Result.Failure("Invoice is already fully paid");
        }

        if (request.Amount <= 0)
        {
            return Result.Failure("Payment amount must be greater than zero");
        }

        if (request.Amount > invoice.Balance)
        {
            return Result.Failure($"Payment amount exceeds invoice balance of {invoice.Balance:C}");
        }

        // Generate payment number
        var paymentCount = await _context.Payments.CountAsync(cancellationToken);
        var paymentNumber = $"PAY-{DateTime.UtcNow:yyyyMMdd}-{(paymentCount + 1):D5}";

        var payment = new Payment
        {
            InvoiceId = request.InvoiceId,
            PaymentNumber = paymentNumber,
            PaymentDate = DateTime.UtcNow,
            Amount = request.Amount,
            Method = request.Method,
            ReferenceNumber = request.ReferenceNumber,
            Status = PaymentStatus.Completed,
            Notes = request.Notes
        };

        _context.Payments.Add(payment);

        // Update invoice
        invoice.PaidAmount += request.Amount;
        
        if (invoice.Balance == 0)
        {
            invoice.Status = InvoiceStatus.Paid;
        }
        else if (invoice.PaidAmount > 0)
        {
            invoice.Status = InvoiceStatus.PartiallyPaid;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success($"Payment of {request.Amount:C} processed successfully");
    }
}
