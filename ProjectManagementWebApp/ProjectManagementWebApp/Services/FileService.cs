using ProjectManagementWebApp.Data;

namespace ProjectManagementWebApp.Services
{

    public class FileService : ServiceBase, IFileService
    {
        public FileService(ProjectManagementContext context, ICurrentUserService currentUserService)
            : base(context, currentUserService)
        {
        }
    }
}
