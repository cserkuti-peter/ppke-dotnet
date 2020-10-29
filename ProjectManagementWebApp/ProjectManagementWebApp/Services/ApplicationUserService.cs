
using Microsoft.AspNetCore.Identity;

using ProjectManagementWebApp.Data;
using ProjectManagementWebApp.Models;

namespace ProjectManagementWebApp.Services
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio
    /// </summary>
    public class ApplicationUserService : ServiceBase, IApplicationUserService
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public ApplicationUserService(
            ProjectManagementContext context,
            ICurrentUserService currentUserService,
            SignInManager<ApplicationUser> signInManager)
            : base(context, currentUserService)
        {
            this.signInManager = signInManager;
        }

        public async System.Threading.Tasks.Task<bool> SignInAsync(string userName, string password)
        {
            var signInResult = await signInManager.PasswordSignInAsync(userName, password, true, false);

            return signInResult.Succeeded;
        }

        public async System.Threading.Tasks.Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
