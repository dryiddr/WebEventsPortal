using AutoMapper;
using Application.Dtos;
using Application.Dtos.Auth;
using Application.Dtos.User;
using Application.Models;
using Domain.User;

namespace Application.Mapping;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<RegisterUserDto, User>()
            .ForMember(user => user.Avatar, 
                opt => opt.Ignore());
        CreateMap<User, AuthDto>();
        CreateMap<User, UserModel>();
    }
}