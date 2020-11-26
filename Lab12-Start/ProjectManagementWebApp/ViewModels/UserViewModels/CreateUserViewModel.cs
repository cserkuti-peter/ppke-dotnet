using ProjectManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.ViewModels.UserViewModels
{
    public class CreateUserViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordRepeat { get; set; }

        public RoleValues Role { get; set; }
    }
}
