using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        public string Name { get; set; }

        public RoleValues Role { get; set; }

        [InverseProperty(nameof(TaskModel.User))]
        public ICollection<TaskModel> Tasks { get; set; }

    }
}
