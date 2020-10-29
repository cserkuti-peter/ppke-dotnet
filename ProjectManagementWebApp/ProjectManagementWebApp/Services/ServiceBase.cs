using ProjectManagementWebApp.Data;

namespace ProjectManagementWebApp.Services
{
    public abstract class ServiceBase
    {
        protected readonly ProjectManagementContext context;
        protected readonly ICurrentUserService currentUserService;

        public ServiceBase(ProjectManagementContext context, ICurrentUserService currentUserService)
        {
            this.context = context;
            this.currentUserService = currentUserService;
        }
    }
}
