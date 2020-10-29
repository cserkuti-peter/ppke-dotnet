using ProjectManagementWebApp.Data;

namespace ProjectManagementWebApp.Services
{
    public class ProjectService : ServiceBase, IProjectService
    {
        public ProjectService(ProjectManagementContext context) : base(context)
        {
        }
    }
}
