using Microsoft.AspNetCore.Authorization;
using Storage.API.Authorization.HelperTypes;

namespace Storage.API.Authorization.Requirements;

public class UserPermissionsRequirement : IAuthorizationRequirement
{
    public UserPermissions Permissions { get; set; }
    public UserPermissionsRequirement(UserPermissions permissions) => Permissions = permissions;

}