using MediatR;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Colleges.Commands.CreateCollege;

public class CreateCollegeCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? DeanName { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
}
