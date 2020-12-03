using AutoMapper;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.ViewModels.TaskViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Profiles.TaskProfiles
{
    public class TaskEditProfile : Profile
    {
        public TaskEditProfile()
        {
            CreateMap<TaskEditViewModel, TaskModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Started, opt => opt.MapFrom(src => src.Started))
            .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Deadline))
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();
        }
    }
}
