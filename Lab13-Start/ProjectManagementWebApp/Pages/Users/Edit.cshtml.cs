using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManagementWebApp.Data;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Services;
using ProjectManagementWebApp.ViewModels.UserViewModels;

namespace ProjectManagementWebApp.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly UserAppService _userAppService;

        public EditModel(UserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [BindProperty]
        public EditUserViewModel UserModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserModel = await _userAppService.GetUserForEditAsync(id.Value);

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

            var res = await _userAppService.EditUserAsync(UserModel);
            if (res == null)
            {
                return NotFound();
            }
            else if (res.Succeeded == false)
            {
                foreach (var err in res.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return Page();
            }

            return RedirectToPage("./Index");
        }

       
    }
}
