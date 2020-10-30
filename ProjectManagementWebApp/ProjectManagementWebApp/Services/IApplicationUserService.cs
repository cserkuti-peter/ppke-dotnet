using ProjectManagementWebApp.Dtos;

namespace ProjectManagementWebApp.Services
{
    public interface IApplicationUserService
    {
        System.Threading.Tasks.Task<PaginatedList<UserViewModel>> GetUsersAsync(string searchString, int? pageIndex);
        System.Threading.Tasks.Task<UserViewModel> GetUserAsync(int? id);
        System.Threading.Tasks.Task CreateUserAsync(CreateUserModel model);
        System.Threading.Tasks.Task<bool> SignInAsync(SignInModel model);
        System.Threading.Tasks.Task SignOutAsync();
        System.Threading.Tasks.Task<bool> DeleteAsync(int id);
        System.Threading.Tasks.Task UpdateAsync(UserViewModel model);
    }
}
