using AutoMapper;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Profiles.UserProfiles
{
    public class UserDetailsProfile : Profile
    {
        public UserDetailsProfile()
        {
            CreateMap<ApplicationUser, UserDetailsViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        }
    }
}
