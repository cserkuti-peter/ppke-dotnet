using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.ViewModels.UserViewModels;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Pages.Users
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ProjectManagementWebApp.Data.ProjectManagementContext _context;

        public CreateModel(SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, ProjectManagementWebApp.Data.ProjectManagementContext context)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }


        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateUserViewModel UserModel { get; set; }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (UserModel.Password != UserModel.PasswordRepeat)
            {
                ModelState.AddModelError("UserModel.Password", "Passwords do not match.");

                return Page();
            }

            var user = new ApplicationUser { Name = UserModel.Name, UserName = UserModel.Email, Email = UserModel.Email, EmailConfirmed = true, Role = UserModel.Role };

            var res = await _signInManager.UserManager.CreateAsync(user, UserModel.Password);
            if (!res.Succeeded)
            {
                foreach (var err in res.Errors)
                {
                    ModelState.AddModelError("UserModel.Password", err.Description);
                }
                return Page();
            }

            if (!await _roleManager.RoleExistsAsync(user.Role.ToString()))
            {
                var roleRes = await _roleManager.CreateAsync(new ApplicationRole { Name = user.Role.ToString() });
                if (!roleRes.Succeeded)
                {
                    foreach (var err in roleRes.Errors)
                    {
                        ModelState.AddModelError("UserModel.Role", err.Description);
                    }
                    return Page();
                }
            }

            var addRoleRes = await _signInManager.UserManager.AddToRoleAsync(user, user.Role.ToString());
            if (!addRoleRes.Succeeded)
            {
                foreach (var err in addRoleRes.Errors)
                {
                    ModelState.AddModelError("UserModel.Role", err.Description);
                }
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
