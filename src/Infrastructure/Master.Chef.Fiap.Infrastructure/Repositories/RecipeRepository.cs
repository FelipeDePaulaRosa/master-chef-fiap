using Master.Chef.Fiap.Infrastructure.Contexts;
using Master.Chef.Fiap.Domain.Entities.Recipes;
using Master.Chef.Fiap.Infrastructure.Repositories.Contracts;

namespace Master.Chef.Fiap.Infrastructure.Repositories;

public class RecipeRepository : Repository<Recipe>, IRecipeRepository
{
    public RecipeRepository(MasterChefApiDbContext context) : base(context)
    {
    }
}