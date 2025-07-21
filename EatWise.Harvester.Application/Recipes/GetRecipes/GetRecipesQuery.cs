using EatWise.Common.Application.Messaging;

namespace EatWise.Harvester.Application.Recipes.GetRecipes;

public sealed record GetRecipesQuery: IQuery<IReadOnlyCollection<RecipeResponse>>;
