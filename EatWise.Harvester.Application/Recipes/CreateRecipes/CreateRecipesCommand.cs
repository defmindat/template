namespace EatWise.Harvester.Application.Recipes.CreateRecipes;

public record CreateRecipesCommand(long[] Ids);
// сделать Parsing на основе Kafka и ключей, если есть такой ключ то не сохранять
