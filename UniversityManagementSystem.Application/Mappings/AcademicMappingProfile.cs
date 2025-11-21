using AutoMapper;
using UniversityManagementSystem.Application.DTOs.Academic;
using UniversityManagementSystem.Core.Entities.Academic;

namespace UniversityManagementSystem.Application.Mappings;

public class AcademicMappingProfile : Profile
{
    public AcademicMappingProfile()
    {
        CreateMap<College, CollegeDto>()
            .ForMember(dest => dest.DepartmentsCount, 
                opt => opt.MapFrom(src => src.Departments.Count));

        CreateMap<Department, DepartmentDto>()
            .ForMember(dest => dest.CollegeName,
                opt => opt.MapFrom(src => src.College.Name))
            .ForMember(dest => dest.CoursesCount,
                opt => opt.MapFrom(src => src.Courses.Count));

        CreateMap<Course, CourseDto>()
            .ForMember(dest => dest.DepartmentName,
                opt => opt.MapFrom(src => src.Department.Name))
            .ForMember(dest => dest.PrerequisiteCourseIds,
                opt => opt.MapFrom(src => src.Prerequisites.Select(p => p.PrerequisiteCourseId).ToList()));

        CreateMap<Semester, SemesterDto>()
            .ForMember(dest => dest.CourseOfferingsCount,
                opt => opt.MapFrom(src => src.CourseOfferings.Count));

        CreateMap<CourseOffering, CourseOfferingDto>()
            .ForMember(dest => dest.CourseCode,
                opt => opt.MapFrom(src => src.Course.Code))
            .ForMember(dest => dest.CourseName,
                opt => opt.MapFrom(src => src.Course.Name))
            .ForMember(dest => dest.SemesterName,
                opt => opt.MapFrom(src => src.Semester.Name))
            .ForMember(dest => dest.LecturerName,
                opt => opt.MapFrom(src => src.Lecturer != null ? src.Lecturer.FirstName + " " + src.Lecturer.LastName : null));

        CreateMap<ClassSchedule, ClassScheduleDto>();
    }
}

