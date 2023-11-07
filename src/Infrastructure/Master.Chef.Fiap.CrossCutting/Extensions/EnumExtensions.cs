using System.ComponentModel;
namespace Master.Chef.Fiap.CrossCutting.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Retorna o valor do atributo Description.
    /// </summary>
    /// <param name="enum"></param>
    /// <returns></returns>
    public static string TryGetDescription(this Enum @enum)
    {
        try
        {
            var attribute = @enum.GetAttribute<DescriptionAttribute>();
            return attribute.Description;
        }
        catch
        {
            return string.Empty;
        }
    }
    
    private static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
    {
        if (value == null)
            throw new ArgumentNullException("value", "value is null.");

        var type = value.GetType();
        var name = Enum.GetName(type, value);
        return type.GetField(name!)!
            .GetCustomAttributes(false)!
            .OfType<TAttribute>()!
            .SingleOrDefault()!;
    }
}