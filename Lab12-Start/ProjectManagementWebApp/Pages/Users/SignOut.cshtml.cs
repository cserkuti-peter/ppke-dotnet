using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementWebApp.Models;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Pages.Users
{
    public class SignOutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public SignOutModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage(".././Index");
        }
    }
}
