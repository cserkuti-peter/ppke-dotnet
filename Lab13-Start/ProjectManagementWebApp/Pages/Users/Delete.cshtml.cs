using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectManagementWebApp.Data;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Services;
using ProjectManagementWebApp.ViewModels.UserViewModels;

namespace ProjectManagementWebApp.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly UserAppService _userAppService;

        public DeleteModel(UserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [BindProperty]
        public UserDetailsViewModel ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser = await _userAppService.GetUserDetailsAsync(id.Value);

            if (ApplicationUser == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var res = await _userAppService.DeleteUserAsync(id.Value);
            if (res == null)
            {
                return NotFound();
            }
            else if (!res.Succeeded)
            {
                throw new Exception("User delete failed.");
            }
            return RedirectToPage("./Index");
        }
    }
}
