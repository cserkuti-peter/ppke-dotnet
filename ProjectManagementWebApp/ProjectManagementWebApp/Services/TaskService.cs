using ProjectManagementWebApp.Data;

namespace ProjectManagementWebApp.Services
{

    public class TaskService : ServiceBase, ITaskService
    {
        public TaskService(ProjectManagementContext context, ICurrentUserService currentUserService)
            : base(context, currentUserService)
        {
        }
    }
}
