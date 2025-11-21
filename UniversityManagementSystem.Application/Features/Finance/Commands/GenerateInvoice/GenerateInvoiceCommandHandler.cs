using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Finance;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Entities.Finance;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Finance.Commands.GenerateInvoice;

public class GenerateInvoiceCommandHandler : IRequestHandler<GenerateInvoiceCommand, Result<InvoiceDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GenerateInvoiceCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<InvoiceDto>> Handle(GenerateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == request.StudentId, cancellationToken);

        if (student == null)
        {
            return Result<InvoiceDto>.Failure("Student not found");
        }

        // Generate invoice number
        var invoiceCount = await _context.Invoices.CountAsync(cancellationToken);
        var invoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{(invoiceCount + 1):D5}";

        var invoice = new Invoice
        {
            StudentId = request.StudentId,
            InvoiceNumber = invoiceNumber,
            IssueDate = request.IssueDate,
            DueDate = request.DueDate,
            Status = InvoiceStatus.Issued,
            Notes = request.Notes,
            PaidAmount = 0
        };

        foreach (var item in request.Items)
        {
            invoice.Items.Add(new InvoiceItem
            {
                Description = item.Description,
                Amount = item.Amount,
                Quantity = item.Quantity
            });
        }

        invoice.TotalAmount = invoice.Items.Sum(i => i.TotalAmount);

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync(cancellationToken);

        var invoiceDto = _mapper.Map<InvoiceDto>(invoice);
        return Result<InvoiceDto>.Success(invoiceDto, "Invoice generated successfully");
    }
}
