using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProjectManagementWebApp.Data;
using ProjectManagementWebApp.Models;

namespace ProjectManagementWebApp.Services
{
    public class ProjectService : ServiceBase, IProjectService
    {
        public ProjectService(ProjectManagementContext context, ICurrentUserService currentUserService)
            : base(context, currentUserService)
        {
        }

        public async Task<ICollection<Project>> GetProjectsAsync(CancellationToken cancellationToken = default)
        {
            var projects = await context.Project.ToListAsync(cancellationToken);

            return projects;
        }
    }
}
