using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementWebApp.Services;
using ProjectManagementWebApp.ViewModels.TaskViewModels;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Pages.Tasks
{
    public class CreateModel : PageModel
    {
        private readonly ProjectManagementWebApp.Data.ProjectManagementContext _context;
        private readonly TaskAppService _taskAppService;

        public CreateModel(ProjectManagementWebApp.Data.ProjectManagementContext context, TaskAppService taskAppService)
        {
            _context = context;
            _taskAppService = taskAppService;
        }

        public IActionResult OnGet(int projectId)
        {
            ViewData["ProjectId"] = projectId;
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public TaskCreateViewModel TaskModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var res = await _taskAppService.TrySaveCreateTaskViewModelAsync(TaskModel);
            if (res)
            {

                return RedirectToPage($"/Projects/Details", new { id = TaskModel.ProjectId });
            }
            else
            {
                return Page();
            }
        }
    }
}
