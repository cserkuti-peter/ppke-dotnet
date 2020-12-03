using AutoMapper;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.ViewModels.TaskViewModels;

namespace ProjectManagementWebApp.Profiles.TaskProfiles
{
    public class TaskDetailsProfile : Profile
    {
        public TaskDetailsProfile()
        {
            CreateMap<TaskModel, TaskDetailsViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Started, opt => opt.MapFrom(src => src.Started))
                .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Deadline))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Project.Name));
        }
    }
}
