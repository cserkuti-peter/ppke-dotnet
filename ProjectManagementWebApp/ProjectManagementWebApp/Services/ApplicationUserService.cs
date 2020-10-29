using ProjectManagementWebApp.Data;

namespace ProjectManagementWebApp.Services
{
    public class ApplicationUserService : ServiceBase, IApplicationUserService
    {
        public ApplicationUserService(ProjectManagementContext context, ICurrentUserService currentUserService)
            : base(context, currentUserService)
        {
        }
    }
}
