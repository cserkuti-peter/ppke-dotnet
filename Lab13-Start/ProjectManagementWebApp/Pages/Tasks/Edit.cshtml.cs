using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementWebApp.Services;
using ProjectManagementWebApp.ViewModels.TaskViewModels;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly ProjectManagementWebApp.Data.ProjectManagementContext _context;
        private readonly TaskAppService _taskAppService;

        public EditModel(ProjectManagementWebApp.Data.ProjectManagementContext context, TaskAppService taskAppService)
        {
            _context = context;
            _taskAppService = taskAppService;
        }

        [BindProperty]
        public TaskEditViewModel TaskModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TaskModel = await _taskAppService.GetTaskViewModelForEditAsync(id.Value);

            if (TaskModel == null)
            {
                return NotFound();
            }
            //ViewData["ProjectId"] = new SelectList(_context.Project, "ID", "ID");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _taskAppService.TrySaveEditTaskViewModelAsync(TaskModel);

            return RedirectToPage($"/Projects/Details", new { id = TaskModel.ProjectId });
        }


    }
}
