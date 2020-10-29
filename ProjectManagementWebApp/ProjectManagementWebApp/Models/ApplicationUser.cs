using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

namespace ProjectManagementWebApp.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        [InverseProperty(nameof(Task.AssignedToUser))]
        public ICollection<Task> Tasks { get; set; }
    }
}
