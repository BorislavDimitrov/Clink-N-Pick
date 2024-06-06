using System.Collections;
using System.Reflection;

namespace ClickNPick.Application.Configurations.Cache;

public class CachePropertyExtractor
{
    public static List<CacheParameter> GetCacheParameters(object? obj)
    {
        var cacheParameters = new List<CacheParameter>();

        if (obj == null)
        {
            return cacheParameters;
        }

        var type = obj.GetType();
        if (IsSimpleType(type))
        {
            cacheParameters.Add(new CacheParameter(type.Name, obj.ToString()));
            return cacheParameters;
        }

        if (obj is IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                cacheParameters.AddRange(GetCacheParameters(item));
            }
            return cacheParameters;
        }

        var propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in propertyInfos)
        {
            var value = property.GetValue(obj, null);
            cacheParameters.AddRange(GetAllPropertiesWithPropertyName(property.Name, value));
        }

        return cacheParameters;
    }

    private static IEnumerable<CacheParameter> GetAllPropertiesWithPropertyName(string propertyName, object? value)
    {
        var cacheParameters = new List<CacheParameter>();

        if (value == null)
        {
            cacheParameters.Add(new CacheParameter(propertyName, "null"));
            return cacheParameters;
        }

        var type = value.GetType();
        if (IsSimpleType(type))
        {
            cacheParameters.Add(new CacheParameter(propertyName, value.ToString()));
            return cacheParameters;
        }

        if (value is IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                cacheParameters.AddRange(GetAllPropertiesWithPropertyName(propertyName, item));
            }
            return cacheParameters;
        }

        var propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in propertyInfos)
        {
            cacheParameters.AddRange(GetAllPropertiesWithPropertyName(property.Name, property.GetValue(value, null)));
        }

        return cacheParameters;
    }

    private static bool IsSimpleType(Type type)
        => type.IsPrimitive ||
           type.IsEnum ||
           type == typeof(string) ||
           type == typeof(decimal) ||
           type == typeof(DateTime);
}
