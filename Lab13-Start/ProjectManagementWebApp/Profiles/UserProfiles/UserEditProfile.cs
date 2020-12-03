using AutoMapper;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Profiles.UserProfiles
{
    public class UserEditProfile : Profile
    {
        public UserEditProfile()
        {
            CreateMap<ApplicationUser, EditUserViewModel>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
               .ReverseMap();
        }
    }
}
