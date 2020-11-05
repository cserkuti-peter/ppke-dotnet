using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectManagementWebApp.Data;
using ProjectManagementWebApp.Models;

namespace ProjectManagementWebApp.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly ProjectManagementWebApp.Data.ProjectManagementContext _context;

        public IndexModel(ProjectManagementWebApp.Data.ProjectManagementContext context)
        {
            _context = context;
        }

        public IList<Project> Project { get;set; }

        public async Task OnGetAsync(string SearchString)
        {
            Project = await _context.Project.ToListAsync();
        }
    }
}
