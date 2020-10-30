using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ProjectManagementWebApp.Dtos;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Services;

namespace ProjectManagementWebApp.Pages.Users
{
    [Authorize(Roles = nameof(Role.Admin))]
    public class IndexModel : PageModel
    {
        private readonly IApplicationUserService service;

        public IndexModel(IApplicationUserService service)
        {
            this.service = service;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? PageIndex { get; set; }

        public PaginatedList<UserViewModel> Users { get; set; }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Users = await service.GetUsersAsync(SearchString, PageIndex);
        }
    }
}
