using ProjectManagementWebApp.Data;

namespace ProjectManagementWebApp.Services
{

    public class FileService : ServiceBase, IFileService
    {
        public FileService(ProjectManagementContext context) : base(context)
        {
        }
    }
}
