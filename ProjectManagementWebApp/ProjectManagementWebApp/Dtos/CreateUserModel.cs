using System.ComponentModel.DataAnnotations;

using ProjectManagementWebApp.Models;

namespace ProjectManagementWebApp.Dtos
{
    public class CreateUserModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
