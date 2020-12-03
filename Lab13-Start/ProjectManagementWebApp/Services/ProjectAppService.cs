using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly CurrentUserService _currentUserService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProjectAppService(ProjectManagementContext context, SignInManager<ApplicationUser> signInManager, IMapper mapper, CurrentUserService currentUserService)
        {
            this._dbContext = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            this._signInManager = signInManager;
        }

        public async Task<PagedList<ProjectIndexViewModel>> GetProjectsPagedListAsync(int pageIndex, int pageSize, string searchString)
        {
            var userId = _currentUserService.UserId;
            var query = _dbContext.Project.AsQueryable();
            var user = await _signInManager.UserManager.FindByIdAsync(userId.ToString());
            if (!(await _signInManager.UserManager.IsInRoleAsync(user, "Admin") || await _signInManager.UserManager.IsInRoleAsync(user, "ProjectManager")))
            {
                query = query.Where(t => t.Tasks.Any(k => k.UserId == userId));
            }
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(p => p.Name.Contains(searchString));
            }
            var dtoRes = await PagedList<Project>.CreatePagedListAsync(query, pageIndex, pageSize);
            return new PagedList<ProjectIndexViewModel>(dtoRes.Select(q => _mapper.Map<ProjectIndexViewModel>(q)).ToList(), dtoRes.TotalPages, pageIndex);
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

            return new ProjectDetailsViewModel
            {
                ProjectDescription = project.Description,
                ProjectId = project.ID,
                ProjectName = project.Name
            };

            //return _mapper.Map<ProjectDetailsViewModel>(project);
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
