using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

namespace ProjectManagementWebApp.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        public string Name { get; set; }

        [InverseProperty(nameof(Task.AssignedToUser))]
        public ICollection<Task> Tasks { get; set; }

        public Role Role { get; set; }

        public string ProfilePicture { get; set; }
    }
}
