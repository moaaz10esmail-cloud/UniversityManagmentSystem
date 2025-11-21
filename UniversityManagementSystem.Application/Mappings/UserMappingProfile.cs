using AutoMapper;
using UniversityManagementSystem.Application.DTOs.Users;
using UniversityManagementSystem.Core.Entities.Identity;

namespace UniversityManagementSystem.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<ApplicationUser, UserDto>();
        CreateMap<ApplicationUser, UserProfileDto>();
        CreateMap<CreateUserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));
    }
}
