using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementWebApp.Services;
using ProjectManagementWebApp.ViewModels;

namespace ProjectManagementWebApp.Pages.Comments
{
    public class DeleteModel : PageModel
    {
        private readonly ProjectManagementWebApp.Data.ProjectManagementContext _context;
        private readonly CommentAppService _commentAppService;

        public DeleteModel(ProjectManagementWebApp.Data.ProjectManagementContext context, CommentAppService commentAppService)
        {
            _context = context;
            _commentAppService = commentAppService;
        }

        [BindProperty]
        public CommentViewModel CommentModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CommentModel = await _commentAppService.GetCommentAsync(id.Value);

            if (CommentModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskId = await _commentAppService.DeleteCommentAsync(id.Value);

            return RedirectToPage($"/Tasks/Details", new { id = taskId });
        }
    }
}
