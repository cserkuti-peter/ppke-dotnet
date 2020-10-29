using Microsoft.AspNetCore.Mvc;

using ProjectManagementWebApp.Services;

namespace ProjectManagementWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
    }
}
