using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementWebApp.Services;
using ProjectManagementWebApp.ViewModels.TaskViewModels;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Pages.Tasks
{
    public class DeleteModel : PageModel
    {
        private readonly ProjectManagementWebApp.Data.ProjectManagementContext _context;
        private readonly TaskAppService _taskAppService;

        public DeleteModel(ProjectManagementWebApp.Data.ProjectManagementContext context, TaskAppService taskAppService)
        {
            _context = context;
            _taskAppService = taskAppService;
        }

        [BindProperty]
        public TaskDetailsViewModel TaskModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TaskModel = await _taskAppService.GetTaskViewModelForDetailsAsync(id.Value);

            if (TaskModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _taskAppService.GetTaskViewModelForDetailsAsync(id.Value);
            await _taskAppService.DeleteTasksAsync(id.Value);

            return RedirectToPage($"/Projects/Details", new { id = TaskModel.ProjectId });
        }
    }
}
