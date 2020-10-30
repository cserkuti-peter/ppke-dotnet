using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ProjectManagementWebApp.Dtos;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Services;

namespace ProjectManagementWebApp.Pages.Users
{
    [Authorize(Roles = nameof(Role.Admin))]
    public class DeleteModel : PageModel
    {
        private readonly IApplicationUserService service;

        public DeleteModel(IApplicationUserService service)
        {
            this.service = service;
        }

        [BindProperty]
        public UserViewModel UserModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            UserModel = await service.GetUserAsync(id);

            if (UserModel == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var found = await service.DeleteAsync(id);

            if (!found)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
