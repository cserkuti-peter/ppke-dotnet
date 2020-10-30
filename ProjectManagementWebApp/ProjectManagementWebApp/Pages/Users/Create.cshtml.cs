using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ProjectManagementWebApp.Dtos;
using ProjectManagementWebApp.Exceptions;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Services;

namespace ProjectManagementWebApp.Pages.Users
{
    [Authorize(Roles = nameof(Role.Admin))]
    public class CreateModel : PageModel
    {
        private readonly IApplicationUserService service;

        public CreateModel(IApplicationUserService service)
        {
            this.service = service;
        }

        [BindProperty]
        public CreateUserModel UserModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await service.CreateUserAsync(UserModel);
            }
            catch (IdentityResultException ire)
            {
                foreach (var error in ire.Result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
