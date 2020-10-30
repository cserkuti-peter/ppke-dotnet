using System.ComponentModel.DataAnnotations;

namespace ProjectManagementWebApp.Dtos
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
