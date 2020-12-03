using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.ViewModels.UserViewModels;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Pages.Users
{
    public class SignInModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public SignInModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public SignInViewModel SignInViewModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var res = await _signInManager.PasswordSignInAsync(SignInViewModel.UserName, SignInViewModel.Password, true, false);
            if (!res.Succeeded)
            {
                ModelState.AddModelError("SignInViewModel.Password", "Login failed");
            }

            return RedirectToPage(".././Index");

        }
    }
}
