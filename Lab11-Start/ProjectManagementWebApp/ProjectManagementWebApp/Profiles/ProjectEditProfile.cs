using AutoMapper;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.ViewModels;

namespace ProjectManagementWebApp.Profiles
{
    public class ProjectEditProfile : Profile
    {
        public ProjectEditProfile()
        {
            CreateMap<Project, ProjectEditViewModel>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProjectDescription, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();
        }
    }
}
