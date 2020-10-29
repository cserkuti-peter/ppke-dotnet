namespace ProjectManagementWebApp.Services
{
    public interface IApplicationUserService
    {
        System.Threading.Tasks.Task<bool> SignInAsync(string userName, string password);
        System.Threading.Tasks.Task SignOutAsync();
    }
}
