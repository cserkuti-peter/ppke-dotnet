using AutoMapper;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.ViewModels;

namespace ProjectManagementWebApp.Profiles
{
    public class ProjectCreateProfile : Profile
    {
        public ProjectCreateProfile()
        {
            CreateMap<ProjectCreateViewModel, Project>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProjectName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProjectDescription));
        }
    }
}
