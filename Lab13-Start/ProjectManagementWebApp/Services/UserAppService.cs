using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProjectManagementWebApp.Data;
using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.ViewModels.UserViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Services
{
    public class UserAppService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ProjectManagementContext _context;


        public UserAppService(
              ProjectManagementContext context,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
                IMapper mapper)
        {
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<UserDetailsViewModel> GetUserDetailsAsync(int userId)
        {
            var user = await _signInManager.UserManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                return this._mapper.Map<UserDetailsViewModel>(user);
            }
            else
            {
                return null;
            }
        }

        public async Task<EditUserViewModel> GetUserForEditAsync(int userId)
        {
            var user = await _signInManager.UserManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                return this._mapper.Map<EditUserViewModel>(user);
            }
            else
            {
                return null;
            }
        }

        public async Task<IdentityResult> EditUserAsync(EditUserViewModel userViewModel)
        {
            var originalUser = await _signInManager.UserManager.FindByIdAsync(userViewModel.Id.ToString());
          
            if (originalUser != null)
            {
                var mappedUser = this._mapper.Map<EditUserViewModel, ApplicationUser>(userViewModel, originalUser);

                return await _signInManager.UserManager.UpdateAsync(mappedUser);
            }
            else
            {
                return null;
            }
        }

        public async Task<PagedList<UserDetailsViewModel>> GetUserListAsync(int pageIndex, int pageSize)
        {
            var query = _context.Users.AsQueryable();
            var dtoRes = await PagedList<ApplicationUser>.CreatePagedListAsync(query, pageIndex, pageSize);
            return new PagedList<UserDetailsViewModel>(dtoRes.Select(q => _mapper.Map<UserDetailsViewModel>(q)).ToList(), dtoRes.TotalPages, pageIndex);
        }

        public async Task<IdentityResult> DeleteUserAsync(int userId)
        {
            var user = await _signInManager.UserManager.FindByIdAsync( userId.ToString());
            if (user != null)
            {
               return await _signInManager.UserManager.DeleteAsync(user);
            }
            else
            {
                return null;
            }
        }
    }
}
