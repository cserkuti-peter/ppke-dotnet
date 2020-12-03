using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementWebApp.Services;
using ProjectManagementWebApp.ViewModels;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly ProjectAppService _projectAppService;
        private readonly TaskAppService _taskAppService;

        public DetailsModel(ProjectAppService projectAppService, TaskAppService taskAppService)
        {
            _projectAppService = projectAppService;
            _taskAppService = taskAppService;
        }

        public ProjectDetailsViewModel Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _projectAppService.GetProjectViewModelForDetailsAsync(id);

            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<PartialViewResult> OnGetTaskListPartial(int? id)
        {
            return Partial("_TaskListPartial", await _taskAppService.ListTasksForProjectAsync(id.Value));
        }
    }
}
