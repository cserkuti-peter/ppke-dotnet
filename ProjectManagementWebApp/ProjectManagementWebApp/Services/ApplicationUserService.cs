using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using ProjectManagementWebApp.Data;
using ProjectManagementWebApp.Dtos;
using ProjectManagementWebApp.Models;

namespace ProjectManagementWebApp.Services
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio
    /// </summary>
    public class ApplicationUserService : ServiceBase, IApplicationUserService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public ApplicationUserService(
            ProjectManagementContext context,
            ICurrentUserService currentUserService,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
            : base(context, currentUserService)
        {
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task<PaginatedList<UserViewModel>> GetUsersAsync(string searchString, int? pageIndex)
        {
            var users = context.Set<ApplicationUser>()
                .Select(x => new UserViewModel { Id = x.Id, Email = x.Email, Name = x.Name, Role = x.Role });

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(x => x.Name.Contains(searchString));
            }

            var pageSize = 3;
            var result = await PaginatedList<UserViewModel>.CreateAsync(users, pageIndex ?? 1, pageSize);

            return result;
        }

        public async System.Threading.Tasks.Task CreateUserAsync(CreateUserModel model)
        {
            var user = new ApplicationUser { Email = model.Email, EmailConfirmed = true, UserName = model.Email, Name = model.Name, Role = model.Role };

            await signInManager.UserManager.CreateAsync(user, model.Password);

            await AddToRoleAsync(user, model.Role);
        }

        public async Task<bool> SignInAsync(SignInModel model)
        {
            var signInResult = await signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

            return signInResult.Succeeded;
        }

        public async System.Threading.Tasks.Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<UserViewModel> GetUserAsync(int? id)
        {
            ApplicationUser user = null;

            if (id.HasValue)
            {
                user = await context.Set<ApplicationUser>().SingleOrDefaultAsync(x => x.Id == id.Value);
            }
            else
            {
                user = await signInManager.UserManager.GetUserAsync(currentUserService.ClaimsPrincipal);
            }

            if (user == null)
            {
                return null;
            }

            return new UserViewModel { Id = user.Id, Email = user.Email, Name = user.Name, Role = user.Role };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await context.Set<ApplicationUser>().SingleOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return false;
            }
            else
            {
                await signInManager.UserManager.DeleteAsync(user);
                return true;
            }
        }

        public async System.Threading.Tasks.Task UpdateAsync(UserViewModel model)
        {
            var user = await context.Set<ApplicationUser>().AsTracking().SingleOrDefaultAsync(x => x.Id == model.Id);

            user.Name = model.Name;

            if (user.Role != model.Role)
            {
                await signInManager.UserManager.RemoveFromRoleAsync(user, user.Role.ToString());
                await AddToRoleAsync(user, model.Role);

                user.Role = model.Role;
            }

            await context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task ChangePasswordAsync(ChangePasswordViewModel model)
        {
            var user = await signInManager.UserManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                throw new Exception();
            }

            var result = await signInManager.UserManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                throw new Exception();
            }
        }

        private async System.Threading.Tasks.Task AddToRoleAsync(ApplicationUser user, Role role)
        {
            if (!await roleManager.RoleExistsAsync(role.ToString()))
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = role.ToString() });
            }

            await signInManager.UserManager.AddToRoleAsync(user, role.ToString());
        }
    }
}
