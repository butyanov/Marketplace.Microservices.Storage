using Microsoft.AspNetCore.Authorization;
using Storage.API.Authorization.HelperTypes;
using Storage.API.Infrastructure.Exceptions;

namespace Storage.API.Authorization.Requirements;

public class UserPermissionsRequirementHandler : AuthorizationHandler<UserPermissionsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        UserPermissionsRequirement requirement)
    {
        if (!context.User.Identity!.IsAuthenticated)
            throw new UnauthorizedException("UNAUTHENTICATED_USER");
        
        if(!IsPermitted(requirement.Permissions, (UserPermissions) uint.Parse(context.User.Claims.First(c => c.Type == "Permissions").Value)))
            throw new UnauthorizedAccessException();
        
        context.Succeed(requirement);
    }

    private bool IsPermitted(UserPermissions requiredPermissions, UserPermissions userPermissions) =>
        (requiredPermissions & userPermissions) == requiredPermissions;
}