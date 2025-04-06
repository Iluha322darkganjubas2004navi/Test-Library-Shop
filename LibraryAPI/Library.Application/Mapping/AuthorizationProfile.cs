using AutoMapper;
using Library.Application.DTOs.Authorization;
using Library.Application.DTOs.User;
using Library.Domain.Entities;
using Library.Domain.Enums;

namespace Library.Application.Mapping;

public class AuthorizationMappingProfile : Profile
{
    public AuthorizationMappingProfile()
    {
        CreateMap<RegisterRequest, User>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserRole.User))
            .ForMember(dest => dest.BorrowedBooks, opt => opt.Ignore());
    }
}
