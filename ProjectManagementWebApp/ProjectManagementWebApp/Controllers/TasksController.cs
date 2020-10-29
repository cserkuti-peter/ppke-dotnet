using Microsoft.AspNetCore.Mvc;

using ProjectManagementWebApp.Services;

namespace ProjectManagementWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService taskService;

        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }
    }
}
