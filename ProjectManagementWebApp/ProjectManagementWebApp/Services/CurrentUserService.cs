using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using ProjectManagementWebApp.Constants;

namespace ProjectManagementWebApp.Services
{
    public class CurrentUserService : ICurrentUserService
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
                var rawValue = ClaimsPrincipal?.FindFirstValue(Claims.APP_USER_ID);

                return rawValue != null && int.TryParse(rawValue, out var value)
                    ? value
                    : (int?)null;
            }
        }
    }
}
