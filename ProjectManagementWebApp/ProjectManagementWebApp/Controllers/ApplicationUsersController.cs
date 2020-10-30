using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ProjectManagementWebApp.Dtos;
using ProjectManagementWebApp.Services;

namespace ProjectManagementWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly IApplicationUserService applicationUserService;

        public ApplicationUsersController(IApplicationUserService applicationUserService)
        {
            this.applicationUserService = applicationUserService;
        }

        [HttpPost("sign-in")]
        public async Task<bool> SignIn(SignInModel model)
        {
            var result = await applicationUserService.SignInAsync(model);

            return result;
        }

        [HttpPost("sign-out")]
        public async System.Threading.Tasks.Task SignOut()
        {
            await applicationUserService.SignOutAsync();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task CreateUser(CreateUserModel model)
        {
            await applicationUserService.CreateUserAsync(model);
        }
    }
}
