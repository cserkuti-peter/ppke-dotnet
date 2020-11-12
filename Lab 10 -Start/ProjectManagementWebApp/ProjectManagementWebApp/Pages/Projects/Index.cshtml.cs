using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly ProjectManagementWebApp.Data.ProjectManagementContext _context;

        public IndexModel(ProjectManagementWebApp.Data.ProjectManagementContext context)
        {
            _context = context;
        }

        public PagedList<Project> Project { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? PageIndex { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Project.AsQueryable();
            if (!string.IsNullOrWhiteSpace(SearchString)) { 
             query = query.Where(p => p.Name.Contains(SearchString));
            }
            var pageIndex = 1;
            if (PageIndex.HasValue && PageIndex > 0)
            {
                pageIndex = PageIndex.Value;
            }
            Project = await PagedList<Project>.CreatePagedListAsync(query, pageIndex, 3);
        }
    }
}
