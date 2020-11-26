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
using ProjectManagementWebApp.ViewModels;

namespace ProjectManagementWebApp.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly ProjectAppService _projectAppService;

        public DetailsModel(ProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        public ProjectDetailsViewModel Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _projectAppService.GetProjectViewModelForDetailsAsync(id);

            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
