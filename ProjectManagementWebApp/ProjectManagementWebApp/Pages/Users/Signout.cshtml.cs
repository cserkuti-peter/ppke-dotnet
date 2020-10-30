using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ProjectManagementWebApp.Services;

namespace ProjectManagementWebApp.Pages.Users
{
    [Authorize]
    public class SignoutModel : PageModel
    {
        private readonly IApplicationUserService service;

        public SignoutModel(IApplicationUserService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await service.SignOutAsync();

            return RedirectToPage(".././Index");
        }
    }
}
