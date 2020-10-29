using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Services;

namespace ProjectManagementWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService projectService;

        public ProjectsController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet]
        public async Task<ICollection<Project>> GetProjectsAsync(CancellationToken cancellationToken)
        {
            return await projectService.GetProjectsAsync(cancellationToken);
        }
    }
}
