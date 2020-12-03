using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Services
{
    public class CurrentUserService
    {
        private readonly HttpContext httpContext;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
        }

        public ClaimsPrincipal ClaimsPrincipal => httpContext?.User;

        public int? UserId
        {
            get
            {
                var rawValue = ClaimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier);

                return rawValue != null && int.TryParse(rawValue, out var value)
                    ? value
                    : (int?)null;
            }
        }
    }
}
