using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Finance;

namespace UniversityManagementSystem.Application.Features.Finance.Queries.GetFinancialSummary;

public class GetFinancialSummaryQuery : IRequest<Result<FinancialSummaryDto>>
{
    public Guid StudentId { get; set; }
}
