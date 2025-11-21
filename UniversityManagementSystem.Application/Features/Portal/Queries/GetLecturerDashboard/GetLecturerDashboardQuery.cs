using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Portal;

namespace UniversityManagementSystem.Application.Features.Portal.Queries.GetLecturerDashboard;

public class GetLecturerDashboardQuery : IRequest<Result<LecturerDashboardDto>>
{
    public Guid LecturerId { get; set; }
}
