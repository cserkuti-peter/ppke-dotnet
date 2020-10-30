using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ProjectManagementWebApp.Dtos;
using ProjectManagementWebApp.Exceptions;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Services;

namespace ProjectManagementWebApp.Pages.Users
{
    [Authorize(Roles = nameof(Role.Admin))]
    public class EditModel : PageModel
    {
        private readonly IApplicationUserService service;
        private readonly IWebHostEnvironment environment;

        public EditModel(IApplicationUserService service, IWebHostEnvironment environment)
        {
            this.service = service;
            this.environment = environment;
        }

        [BindProperty]
        public UserViewModel UserModel { get; set; }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                UserModel = await service.GetUserAsync(id);
            }
            catch (IdentityResultException ire)
            {
                foreach (var error in ire.Result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return Page();
            }

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

        public async Task<IActionResult> OnPostFileAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var fileId = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(Upload.FileName);
            var path = Path.Combine(environment.WebRootPath, "images", $"{fileId}{extension}");

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }

            var fileName = Path.GetFileName(path);
            await service.UpdateProfilePictureAsync(id, fileName);

            return RedirectToPage(new { Id = id });
        }
    }
}
