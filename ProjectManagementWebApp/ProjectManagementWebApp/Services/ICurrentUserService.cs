using System.Security.Claims;

namespace ProjectManagementWebApp.Services
{
    public interface ICurrentUserService
    {
        ClaimsPrincipal ClaimsPrincipal { get; }

        int? UserId { get; }
    }
}
