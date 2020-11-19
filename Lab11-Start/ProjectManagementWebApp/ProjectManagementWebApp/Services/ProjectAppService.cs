using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManagementWebApp.Data;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Services
{
    public class ProjectAppService
    {
        private readonly ProjectManagementContext _dbContext;
        private readonly IMapper _mapper;

        public ProjectAppService(ProjectManagementContext context, IMapper mapper)
        {
            this._dbContext = context;
            _mapper = mapper;
        }

        public async Task<PagedList<ProjectDetailsViewModel>> GetProjectsPagedListAsync(int pageIndex, int pageSize, string searchString)
        {
            var query = _dbContext.Project.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(p => p.Name.Contains(searchString));
            }
            var dtoRes = await PagedList<Project>.CreatePagedListAsync(query, pageIndex, pageSize);
            return new PagedList<ProjectDetailsViewModel>(dtoRes.Select(q => _mapper.Map<ProjectDetailsViewModel>(q)).ToList(), dtoRes.TotalPages, pageIndex);
        }

        public async Task<ProjectEditViewModel> GetProjectViewModelForEditAsync(int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }

            var project = await _dbContext.Project.FirstOrDefaultAsync(m => m.ID == id);

            if (project == null)
            {
                return null;
            }

            return _mapper.Map<ProjectEditViewModel>(project);
        }

        public async Task<ProjectDetailsViewModel> GetProjectViewModelForDetailsAsync(int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }

            var project = await _dbContext.Project.FirstOrDefaultAsync(m => m.ID == id);

            if (project == null)
            {
                return null;
            }

            return _mapper.Map<ProjectDetailsViewModel>(project);
        }

        public async Task DeleteProjectsAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("project id null");
            }

            var project = await _dbContext.Project.FirstOrDefaultAsync(m => m.ID == id);

            if (project == null)
            {
                throw new Exception("project not found");
            }

            _dbContext.Project.Remove(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> TrySaveEditProjectViewModelAsync(ProjectEditViewModel projectEditViewModel)
        {
            try
            {
                var project = await _dbContext.Project.FirstOrDefaultAsync(m => m.ID == projectEditViewModel.ProjectId);
                project = _mapper.Map<ProjectEditViewModel, Project>(projectEditViewModel, project);

                _dbContext.Attach(project).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
        }



        public async Task<bool> TrySaveCreateProjectViewModelAsync(ProjectCreateViewModel projectCreateViewModel)
        {
            try
            {
              
                var project = _mapper.Map<ProjectCreateViewModel, Project>(projectCreateViewModel);

                _dbContext.Project.Add(project);
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
