using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ProjectManagementWebApp.Dtos;
using ProjectManagementWebApp.Services;

namespace ProjectManagementWebApp.Pages.Users
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IApplicationUserService service;

        public DetailsModel(IApplicationUserService service)
        {
            this.service = service;
        }

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
    }
}
