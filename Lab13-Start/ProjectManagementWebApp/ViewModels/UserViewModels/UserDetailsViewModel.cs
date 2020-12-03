using ProjectManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.ViewModels.UserViewModels
{
    public class UserDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public RoleValues Role { get; set; }
    }
}
