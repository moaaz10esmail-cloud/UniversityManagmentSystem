using AutoMapper;
using UniversityManagementSystem.Application.DTOs.Attendance;
using UniversityManagementSystem.Application.DTOs.Dashboard;
using UniversityManagementSystem.Application.DTOs.Finance;
using UniversityManagementSystem.Application.DTOs.Grades;
using UniversityManagementSystem.Application.DTOs.HR;
using UniversityManagementSystem.Application.DTOs.Library;
using UniversityManagementSystem.Application.DTOs.Portal;
using UniversityManagementSystem.Application.DTOs.Registration;
using UniversityManagementSystem.Core.Entities.Academic;
using UniversityManagementSystem.Core.Entities.Finance;
using UniversityManagementSystem.Core.Entities.HR;
using UniversityManagementSystem.Core.Entities.Library;

namespace UniversityManagementSystem.Application.Mappings;

public class SystemMappingProfile : Profile
{
    public SystemMappingProfile()
    {
        // Student & Registration Mappings
        CreateMap<Student, StudentRegistrationDto>()
            .ForMember(d => d.StudentName, opt => opt.MapFrom(s => $"{s.User.FirstName} {s.User.LastName}"));

        CreateMap<StudentRegistration, StudentRegistrationDto>()
            .ForMember(d => d.StudentName, opt => opt.MapFrom(s => $"{s.Student.User.FirstName} {s.Student.User.LastName}"))
            .ForMember(d => d.CourseCode, opt => opt.MapFrom(s => s.CourseOffering.Course.Code))
            .ForMember(d => d.CourseName, opt => opt.MapFrom(s => s.CourseOffering.Course.Name))
            .ForMember(d => d.CreditHours, opt => opt.MapFrom(s => s.CourseOffering.Course.CreditHours))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type.ToString()))
            .ForMember(d => d.InstructorName, opt => opt.MapFrom(s => 
                s.CourseOffering.Instructor != null 
                    ? $"{s.CourseOffering.Instructor.FirstName} {s.CourseOffering.Instructor.LastName}" 
                    : null));

        // Attendance Mappings
        CreateMap<Attendance, AttendanceDto>()
            .ForMember(d => d.StudentName, opt => opt.MapFrom(s => $"{s.Student.User.FirstName} {s.Student.User.LastName}"))
            .ForMember(d => d.CourseCode, opt => opt.MapFrom(s => s.CourseOffering.Course.Code))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));

        // Finance Mappings
        CreateMap<Invoice, InvoiceDto>()
            .ForMember(d => d.StudentName, opt => opt.MapFrom(s => $"{s.Student.FirstName} {s.Student.LastName}"))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));

        CreateMap<InvoiceItem, InvoiceItemDto>();

        CreateMap<Payment, PaymentDto>()
            .ForMember(d => d.Method, opt => opt.MapFrom(s => s.Method.ToString()))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));

        // Library Mappings
        CreateMap<Book, BookDto>()
            .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category.ToString()));

        CreateMap<BookLoan, BookLoanDto>()
            .ForMember(d => d.BookTitle, opt => opt.MapFrom(s => s.Book.Title))
            .ForMember(d => d.ISBN, opt => opt.MapFrom(s => s.Book.ISBN))
            .ForMember(d => d.StudentName, opt => opt.MapFrom(s => $"{s.Student.FirstName} {s.Student.LastName}"))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.DaysOverdue, opt => opt.MapFrom(s => 
                s.ReturnDate == null && s.DueDate < DateTime.UtcNow 
                    ? (DateTime.UtcNow - s.DueDate).Days 
                    : 0));

        // HR Mappings
        CreateMap<Staff, StaffDto>()
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.User.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.User.LastName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email))
            .ForMember(d => d.DepartmentName, opt => opt.MapFrom(s => s.Department.Name))
            .ForMember(d => d.Position, opt => opt.MapFrom(s => s.Position.ToString()))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));

        CreateMap<LeaveRequest, LeaveRequestDto>()
            .ForMember(d => d.StaffName, opt => opt.MapFrom(s => $"{s.Staff.User.FirstName} {s.Staff.User.LastName}"))
            .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type.ToString()))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));

        // Grade Mappings
        CreateMap<Grade, GradeDto>()
            .ForMember(d => d.StudentName, opt => opt.MapFrom(s => $"{s.StudentRegistration.Student.User.FirstName} {s.StudentRegistration.Student.User.LastName}"))
            .ForMember(d => d.CourseCode, opt => opt.MapFrom(s => s.CourseOffering.Course.Code))
            .ForMember(d => d.CourseName, opt => opt.MapFrom(s => s.CourseOffering.Course.Name))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));

        CreateMap<Grade, TranscriptCourseDto>()
            .ForMember(d => d.CourseCode, opt => opt.MapFrom(s => s.CourseOffering.Course.Code))
            .ForMember(d => d.CourseName, opt => opt.MapFrom(s => s.CourseOffering.Course.Name))
            .ForMember(d => d.CreditHours, opt => opt.MapFrom(s => s.CourseOffering.Course.CreditHours));
    }
}
