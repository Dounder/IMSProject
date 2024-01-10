using AutoMapper;
using IMS.Application.UseCases.Auth.DTOs;
using IMS.Application.UseCases.Users.Commands;
using IMS.Application.UseCases.Users.DTOs;
using IMS.Domain.Entities;

namespace IMS.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, AuthDto>();

        CreateMap<UpdateUserCommand, User>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));

        CreateMap<UserRole, RoleDto>();
    }
}