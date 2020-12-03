using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Services;
using ProjectManagementWebApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly ProjectAppService _projectAppService;

        public IndexModel(ProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        public PagedList<ProjectIndexViewModel> Project { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? PageIndex { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var pageIndex = 1;
            if (PageIndex.HasValue && PageIndex > 0)
            {
                pageIndex = PageIndex.Value;
            }
            Project =  await _projectAppService.GetProjectsPagedListAsync(pageIndex, 3, SearchString);
        }
    }
}
