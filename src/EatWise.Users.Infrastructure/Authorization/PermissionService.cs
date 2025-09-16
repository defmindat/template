using EatWise.Common.Application.Authorization;
using EatWise.Common.Domain;
using EatWise.Users.Application.Users.GetUserPermissions;
using MediatR;

namespace EatWise.Users.Infrastructure.Authorization;

internal sealed class PermissionService(ISender sender): IPermissionService
{
    public Task<Result<PermissionResponse>> GetUserPermissionsAsync(string identityId) =>
        sender.Send(new GetUserPermissionsQuery(identityId));
}
