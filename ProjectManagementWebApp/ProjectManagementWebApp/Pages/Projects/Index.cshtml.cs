using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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

        public PaginatedList<Project> Project { get; set; }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            var projects = _context.Project.Select(x => x);

            if (!string.IsNullOrEmpty(SearchString))
            {
                projects = projects.Where(x => x.Name.Contains(SearchString));
            }

            var pageSize = 3;
            Project = await PaginatedList<Project>.CreateAsync(projects.AsNoTracking(), PageIndex ?? 1, pageSize);
        }
    }
}
