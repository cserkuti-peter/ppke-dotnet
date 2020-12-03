using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManagementWebApp.Data;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Services;
using ProjectManagementWebApp.ViewModels.TaskViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Services
{
    public class TaskAppService
    {
        private readonly ProjectManagementContext _dbContext;
        private readonly IMapper _mapper;
        private readonly CurrentUserService _currentUserService;

        public TaskAppService(ProjectManagementContext context, IMapper mapper, CurrentUserService currentUserService)
        {
            this._dbContext = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<List<TaskDetailsViewModel>> ListTasksForProjectAsync(int projectId)
        {
            return (await _dbContext.Tasks.Where(t=>t.ProjectId == projectId).ToListAsync()).Select(k => _mapper.Map<TaskDetailsViewModel>(k)).ToList();
        }

        public async Task<TaskEditViewModel> GetTaskViewModelForEditAsync(int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }

            var project = await _dbContext.Tasks.FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return null;
            }

            return _mapper.Map<TaskEditViewModel>(project);
        }

        public async Task<TaskDetailsViewModel> GetTaskViewModelForDetailsAsync(int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }

            var task = await _dbContext.Tasks.FirstOrDefaultAsync(m => m.Id == id);

            if (task == null)
            {
                return null;
            }

            return _mapper.Map<TaskDetailsViewModel>(task);
        }

        public async Task DeleteTasksAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("task id null");
            }

            var task = await _dbContext.Tasks.FirstOrDefaultAsync(m => m.Id == id);

            if (task == null)
            {
                throw new Exception("task not found");
            }

            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> TrySaveEditTaskViewModelAsync(TaskEditViewModel taskEditViewModel)
        {
            try
            {
                var task = await _dbContext.Tasks.FirstOrDefaultAsync(m => m.Id == taskEditViewModel.Id);
                task = _mapper.Map<TaskEditViewModel, TaskModel>(taskEditViewModel, task);

                _dbContext.Attach(task).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
        }



        public async Task<bool> TrySaveCreateTaskViewModelAsync(TaskCreateViewModel taskCreateViewModel)
        {
            try
            {

                var task = _mapper.Map<TaskCreateViewModel, TaskModel>(taskCreateViewModel);

                _dbContext.Tasks.Add(task);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
