using AutoMapper;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.ViewModels.TaskViewModels;

namespace ProjectManagementWebApp.Profiles.TaskProfiles
{
    public class TaskCreateProfile : Profile
    {
        public TaskCreateProfile()
        {
            CreateMap<TaskCreateViewModel, TaskModel>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.Started, opt => opt.MapFrom(src => src.Started))
               .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Deadline))
               .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
