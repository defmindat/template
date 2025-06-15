namespace EatWise.Harvester.Application.Recipes.GetRecipes;

public sealed record RecipeResponse(
    Guid Id,
    string Name,
    string Text,
    DateTime CreatedDate
    );
