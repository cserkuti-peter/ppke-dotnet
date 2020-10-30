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
    public class EditModel : PageModel
    {
        private readonly IApplicationUserService service;

        public EditModel(IApplicationUserService service)
        {
            this.service = service;
        }

        [BindProperty]
        public UserViewModel UserModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserModel = await service.GetUserAsync(id);

            if (UserModel == null)
            {
                return NotFound();
            }

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

            await service.UpdateAsync(UserModel);

            return RedirectToPage("./Index");
        }
    }
}
