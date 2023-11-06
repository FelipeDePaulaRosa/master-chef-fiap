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
}

public enum DifficultyLevelEnum : short
{
    Easy = 1,
    Medium = 2,
    Hard = 3
}