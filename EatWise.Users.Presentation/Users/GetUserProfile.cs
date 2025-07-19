using System.Security.Claims;
using EatWise.Common.Domain;
using EatWise.Common.Infrastructure.Authentication;
using EatWise.Common.Presentation.Endpoints;
using EatWise.Common.Presentation.Results;
using EatWise.Users.Application.Users.GetUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EatWise.Users.Presentation.Users;

internal sealed class GetUserProfile: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/profile", async (ClaimsPrincipal claims, ISender sender)  =>
        {
            Result<UserResponse> result = await sender.Send(new GetUserQuery(claims.GetUserId()));
            
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization("users:read")
        .WithTags(Tags.Users);
    }
}
