using MediatR;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Semesters.Commands.CreateSemester;

public class CreateSemesterCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public SemesterType Type { get; set; }
    public int AcademicYear { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime RegistrationStartDate { get; set; }
    public DateTime RegistrationEndDate { get; set; }
    public DateTime AddDropDeadline { get; set; }
    public DateTime WithdrawalDeadline { get; set; }
}
