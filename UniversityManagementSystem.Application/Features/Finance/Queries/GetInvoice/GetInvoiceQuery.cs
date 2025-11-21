using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Finance;

namespace UniversityManagementSystem.Application.Features.Finance.Queries.GetInvoice;

public class GetInvoiceQuery : IRequest<Result<InvoiceDto>>
{
    public Guid InvoiceId { get; set; }
}
