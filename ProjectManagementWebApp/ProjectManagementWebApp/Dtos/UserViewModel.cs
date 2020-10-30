using ProjectManagementWebApp.Models;

namespace ProjectManagementWebApp.Dtos
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public string ProfilePicture { get; set; }
    }
}
