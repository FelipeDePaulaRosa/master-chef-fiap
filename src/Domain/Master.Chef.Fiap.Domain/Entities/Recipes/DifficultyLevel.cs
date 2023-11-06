using System.ComponentModel;
using Master.Chef.Fiap.CrossCutting.Extensions;

namespace Master.Chef.Fiap.Domain.Entities.Recipes;

public class DifficultyLevel : ValueObject
{
    public DifficultyLevelEnum Level { get; private set; }

    public DifficultyLevel(DifficultyLevelEnum level)
    {
        Level = level;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Level;
    }

    public string GetDescription()
    {
        return Level.TryGetDescription();
    }
}

public enum DifficultyLevelEnum : short
{
    [Description("Fácil")]
    Easy = 1,
    
    [Description("Médio")]
    Medium = 2,
    
    [Description("Difícil")]
    Hard = 3
}