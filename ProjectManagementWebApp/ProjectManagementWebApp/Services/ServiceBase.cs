
using ProjectManagementWebApp.Data;

namespace ProjectManagementWebApp.Services
{
    public abstract class ServiceBase
    {
        protected readonly ProjectManagementContext context;

        public ServiceBase(ProjectManagementContext context)
        {
            this.context = context;
        }
    }
}
