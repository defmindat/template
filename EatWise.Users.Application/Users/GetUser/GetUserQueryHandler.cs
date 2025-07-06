using System.Data.Common;
using Dapper;
using EatWise.Common.Application.Contracts;
using EatWise.Common.Application.Data;
using EatWise.Common.Domain;
using EatWise.Users.Domain.Users;

namespace EatWise.Users.Application.Users.GetUser;

internal sealed class GetUserQueryHandler(IDbConnectionFactory dbConnectionFactory): IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = 
            $"""
             SELECT
                id AS {nameof(UserResponse.Id)},
                email AS {nameof(UserResponse.Email)},
                first_name AS {nameof(UserResponse.FirstName)},
                last_name AS {nameof(UserResponse.LastName)}
             FROM users.users
             WHERE id = @UserId
             """;
        
        UserResponse? user = await connection.QuerySingleOrDefaultAsync<UserResponse>(sql, request);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(request.UserId));
        }

        return user;
    }
}
