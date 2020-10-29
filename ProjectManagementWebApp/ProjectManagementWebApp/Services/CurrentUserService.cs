using System.Security.Claims;

using Microsoft.AspNetCore.Http;

namespace ProjectManagementWebApp.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly HttpContext httpContext;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
        }

        public ClaimsPrincipal ClaimsPrincipal => httpContext.User;
    }
}
