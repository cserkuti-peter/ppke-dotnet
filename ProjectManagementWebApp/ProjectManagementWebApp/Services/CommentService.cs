using ProjectManagementWebApp.Data;

namespace ProjectManagementWebApp.Services
{
    public class CommentService : ServiceBase, ICommentService
    {
        public CommentService(ProjectManagementContext context) : base(context)
        {
        }
    }
}
