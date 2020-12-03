using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementWebApp.Services;
using ProjectManagementWebApp.ViewModels.TaskViewModels;
using System.IO;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Pages.Tasks
{
    public class DetailsModel : PageModel
    {
        private readonly ProjectManagementWebApp.Data.ProjectManagementContext _context;
        private readonly TaskAppService _taskAppService;
        private readonly FileAppService _fileAppService;
        private readonly IWebHostEnvironment _environment;

        public DetailsModel(ProjectManagementWebApp.Data.ProjectManagementContext context,
            TaskAppService taskAppService,
            FileAppService fileAppService,
            IWebHostEnvironment environment
            )
        {
            _context = context;
            _taskAppService = taskAppService;
            _fileAppService = fileAppService;
            this._environment = environment;
        }

        public TaskDetailsViewModel TaskModel { get; set; }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TaskModel = await _taskAppService.GetTaskViewModelForDetailsAsync(id.Value);
            ViewData["ProjectId"] = TaskModel.ProjectId;

            if (TaskModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<PartialViewResult> OnGetFileListPartial(int? id)
        {
            return Partial("_FileListPartial", await _fileAppService.ListTaskFilesASync(id.Value));
        }

        public async Task<IActionResult> OnPostFileAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var extension = Path.GetExtension(Upload.FileName);
            var path = Path.Combine(_environment.WebRootPath, "taskfiles", Upload.FileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }

            var fileName = Path.GetFileName(path);
            await _fileAppService.UploadFileAsync(id, fileName);

            return RedirectToPage(new { Id = id });
        }

        public async Task<IActionResult> OnPostDeleteFileAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var taskId = await _fileAppService.DeleteFileAsync(id);

            return RedirectToPage(new { Id = taskId });
        }
    }
}
