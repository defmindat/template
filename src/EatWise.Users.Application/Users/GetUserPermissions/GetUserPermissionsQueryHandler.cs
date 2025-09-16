using System.Data.Common;
using Dapper;
using EatWise.Common.Application.Authorization;
using EatWise.Common.Application.Messaging;
using EatWise.Common.Application.Data;
using EatWise.Common.Domain;
using EatWise.Users.Domain.Users;

namespace EatWise.Users.Application.Users.GetUserPermissions;

internal sealed class GetUserPermissionsQueryHandler(IDbConnectionFactory dbConnectionFactory): IQueryHandler<GetUserPermissionsQuery, PermissionResponse>
{
    public async Task<Result<PermissionResponse>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = $"""
                            SELECT DISTINCT
                                u.id AS {nameof(UserPermission.UserId)},
                                rp.permission_code AS {nameof(UserPermission.Permission)}
                            FROM users.users u
                            JOIN users.user_roles ur ON ur.user_id = u.id
                            JOIN users.role_permissions rp ON rp.role_name = ur.role_name
                            WHERE u.identity_id = @IdentityId 
                            """;
        
        List<UserPermission> permissions = (await connection.QueryAsync<UserPermission>(sql, request)).AsList();

        if (!permissions.Any())
        {
            return Result.Failure<PermissionResponse>(UserErrors.NotFound(request.IdentityId));
        }

        return new PermissionResponse(permissions[0].UserId, permissions.Select(rp => rp.Permission).ToHashSet());
    }

    internal sealed class UserPermission
    {
        internal Guid UserId { get; init; }
        internal string Permission { get; init; }
    }
}
