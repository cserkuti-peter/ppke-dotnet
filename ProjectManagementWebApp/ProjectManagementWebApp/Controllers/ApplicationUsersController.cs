using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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

        [HttpPost("sign-in")]
        public async Task<bool> SignIn([Required, FromQuery] string userName, [Required, FromQuery] string password)
        {
            var result = await applicationUserService.SignInAsync(userName, password);

            return result;
        }

        [HttpPost("sign-out")]
        public async Task SignOut()
        {
            await applicationUserService.SignOutAsync();
        }
    }
}
