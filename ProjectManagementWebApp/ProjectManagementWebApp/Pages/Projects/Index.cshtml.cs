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
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? PageIndex { get; set; }

        private readonly ProjectManagementWebApp.Data.ProjectManagementContext _context;

        public IndexModel(ProjectManagementWebApp.Data.ProjectManagementContext context)
        {
            _context = context;
        }

        public PaginatedList<Project> Project { get;set; }

        public async Task OnGetAsync()
        {
            var projects = _context.Project.Select(x => x);

            if (!String.IsNullOrEmpty(SearchString))
            {
                projects = projects.Where(x => x.Name.Contains(SearchString));
            }

            int pageSize = 3;
            Project = await PaginatedList<Project>.CreateAsync(projects.AsNoTracking(), PageIndex ?? 1, pageSize);
        }
    }
}
