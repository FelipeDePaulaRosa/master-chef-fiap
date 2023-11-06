namespace Master.Chef.Fiap.Domain.Entities.Recipes;

public class DifficultyLevel : ValueObject
{
    public DificultyLevelEnum Level { get; private set; }

    public DifficultyLevel(DificultyLevelEnum level)
    {
        Level = level;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Level;
    }
}

public enum DificultyLevelEnum : short
{
    Easy = 1,
    Medium = 2,
    Hard = 3
}