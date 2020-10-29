using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using ProjectManagementWebApp.Models;

namespace ProjectManagementWebApp.Services
{
    public interface IProjectService
    {
        Task<ICollection<Project>> GetProjectsAsync(CancellationToken cancellationToken = default);
    }
}
