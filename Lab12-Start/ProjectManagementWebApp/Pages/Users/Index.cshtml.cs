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
    public class IndexModel : PageModel
    {
        private readonly UserAppService _userAppService;

        public IndexModel(UserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public PagedList<UserDetailsViewModel> ApplicationUser { get;set; }

        [BindProperty(SupportsGet = true)]
        public int? PageIndex { get; set; }

        public async Task OnGetAsync()
        {
            var pageIndex = 1;
            if (PageIndex.HasValue && PageIndex > 0)
            {
                pageIndex = PageIndex.Value;
            }
            ApplicationUser = await _userAppService.GetUserListAsync(pageIndex, 10);
        }
    }
}
