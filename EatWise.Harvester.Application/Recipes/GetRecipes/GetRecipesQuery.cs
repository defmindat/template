using EatWise.Common.Application.Contracts;

namespace EatWise.Harvester.Application.Recipes.GetRecipes;

public sealed record GetRecipesQuery: IQuery<IReadOnlyCollection<RecipeResponse>>;
