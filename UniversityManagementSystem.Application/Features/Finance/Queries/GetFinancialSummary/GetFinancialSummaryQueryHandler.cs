using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Finance;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Finance.Queries.GetFinancialSummary;

public class GetFinancialSummaryQueryHandler : IRequestHandler<GetFinancialSummaryQuery, Result<FinancialSummaryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFinancialSummaryQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<FinancialSummaryDto>> Handle(GetFinancialSummaryQuery request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == request.StudentId, cancellationToken);

        if (student == null)
        {
            return Result<FinancialSummaryDto>.Failure("Student not found");
        }

        var invoices = await _context.Invoices
            .Include(i => i.Items)
            .Include(i => i.Payments)
            .Where(i => i.StudentId == request.StudentId)
            .OrderByDescending(i => i.IssueDate)
            .ToListAsync(cancellationToken);

        var summary = new FinancialSummaryDto
        {
            StudentId = student.Id,
            StudentName = $"{student.User.FirstName} {student.User.LastName}",
            TotalInvoiced = invoices.Sum(i => i.TotalAmount),
            TotalPaid = invoices.Sum(i => i.PaidAmount),
            TotalBalance = invoices.Sum(i => i.Balance),
            PendingInvoices = invoices.Count(i => i.Status == InvoiceStatus.Issued || i.Status == InvoiceStatus.PartiallyPaid),
            OverdueInvoices = invoices.Count(i => i.DueDate < DateTime.UtcNow && i.Balance > 0),
            RecentInvoices = _mapper.Map<List<InvoiceDto>>(invoices.Take(5))
        };

        return Result<FinancialSummaryDto>.Success(summary);
    }
}
