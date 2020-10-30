using System;

using Microsoft.AspNetCore.Identity;

namespace ProjectManagementWebApp.Exceptions
{
    public class IdentityResultException : Exception
    {
        public IdentityResult Result { get; set; }
    }
}
