using Microsoft.AspNetCore.Mvc;

using ProjectManagementWebApp.Services;

namespace ProjectManagementWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly IApplicationUserService applicationUserService;

        public ApplicationUsersController(IApplicationUserService applicationUserService)
        {
            this.applicationUserService = applicationUserService;
        }
    }
}
