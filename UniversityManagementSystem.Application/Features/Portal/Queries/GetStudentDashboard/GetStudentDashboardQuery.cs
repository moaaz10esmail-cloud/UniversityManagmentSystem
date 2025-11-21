using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Portal;

namespace UniversityManagementSystem.Application.Features.Portal.Queries.GetStudentDashboard;

public class GetStudentDashboardQuery : IRequest<Result<StudentDashboardDto>>
{
    public Guid StudentId { get; set; }
}
