using EatWise.Common.Application.Authorization;
using EatWise.Common.Application.Contracts;

namespace EatWise.Users.Application.Users.GetUserPermissions;

public sealed record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionResponse>;
