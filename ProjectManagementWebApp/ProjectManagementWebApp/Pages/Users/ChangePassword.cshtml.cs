using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ProjectManagementWebApp.Dtos;
using ProjectManagementWebApp.Exceptions;
using ProjectManagementWebApp.Services;

namespace ProjectManagementWebApp.Pages.Users
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        private readonly IApplicationUserService service;

        public ChangePasswordModel(IApplicationUserService service)
        {
            this.service = service;
        }

        [BindProperty]
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userModel = await service.GetUserAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }

            ChangePasswordViewModel = new ChangePasswordViewModel { Id = userModel.Id, Email = userModel.Email };

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
                await service.ChangePasswordAsync(ChangePasswordViewModel);
            }
            catch (IdentityResultException ire)
            {
                foreach (var error in ire.Result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return Page();
            }

            return RedirectToPage("./Details", ChangePasswordViewModel.Id);
        }
    }
}
