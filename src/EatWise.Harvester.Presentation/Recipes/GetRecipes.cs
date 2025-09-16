using EatWise.Common.Domain;
using EatWise.Common.Presentation.Endpoints;
using EatWise.Common.Presentation.Results;
using EatWise.Harvester.Application.Recipes.GetRecipes;
using MediatR;

namespace EatWise.Harvester.Presentation.Recipes;

internal sealed class GetRecipes: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("recipes", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<RecipeResponse>> result = await sender.Send(new GetRecipesQuery());
            
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Recipes);
    }
}
