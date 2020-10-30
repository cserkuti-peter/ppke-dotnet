using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ProjectManagementWebApp.Dtos;
using ProjectManagementWebApp.Services;

namespace ProjectManagementWebApp.Pages.Users
{
    [AllowAnonymous]
    public class SigninModel : PageModel
    {
        private readonly IApplicationUserService service;

        public SigninModel(IApplicationUserService service)
        {
            this.service = service;
        }

        [BindProperty]
        public SignInModel Model { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await service.SignInAsync(Model);

            return RedirectToPage(".././Index");
        }
    }
}
