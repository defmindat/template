using System.Data.Common;
using Dapper;
using EatWise.Common.Application.Data;
using EatWise.Common.Domain;
using EatWise.Common.Application.Messaging;

namespace EatWise.Harvester.Application.Recipes.GetRecipes;

internal sealed class GetRecipesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRecipesQuery, IReadOnlyCollection<RecipeResponse>>
{
    public async Task<Result<IReadOnlyCollection<RecipeResponse>>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = $"""
                             SELECT
                                 id AS {nameof(RecipeResponse.Id)},
                                 name AS {nameof(RecipeResponse.Name)},
                                 text AS {nameof(RecipeResponse.Text)},
                                 createdDate AS {nameof(RecipeResponse.CreatedDate)}
                             FROM harvesters.recipes
                             """;
        
        List<RecipeResponse> recipes = (await connection.QueryAsync<RecipeResponse>(sql, request)).AsList();
        return recipes;
    }
}
