using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementWebApp.Services;
using ProjectManagementWebApp.ViewModels;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Pages.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly ProjectAppService _projectAppService;

        public DeleteModel(ProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _projectAppService.DeleteProjectsAsync(id);

            return RedirectToPage("./Index");
        }
    }
}
