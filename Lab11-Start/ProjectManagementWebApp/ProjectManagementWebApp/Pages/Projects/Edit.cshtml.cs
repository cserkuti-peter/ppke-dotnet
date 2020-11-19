using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementWebApp.Services;
using ProjectManagementWebApp.ViewModels;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly ProjectAppService _projectAppService;

        public EditModel(ProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        [BindProperty(SupportsGet = true)]
        public int? id { get; set; }

        [BindProperty]
        public ProjectEditViewModel ProjectViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var res = await _projectAppService.GetProjectViewModelForEditAsync(id);

            if (res == null)
            {
                return NotFound();
            }
            ProjectViewModel = res;

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
            if (await this._projectAppService.TrySaveEditProjectViewModelAsync(this.ProjectViewModel))
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return NotFound();
            }

           
        }


    }
}
