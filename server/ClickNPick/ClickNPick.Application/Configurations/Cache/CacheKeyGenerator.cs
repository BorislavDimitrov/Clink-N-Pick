namespace ClickNPick.Application.Configurations.Cache;

public static class CacheKeyGenerator<T>
where T : ICacheable
{
    public static string GenerateCacheKey(string prefix, params CacheParameterCollection<T>[] parameterCollections)
        => $"{prefix}:{GetTypeName()}:{string.Join(":", parameterCollections.Select(x => x.ToString()))}";

    private static string GetTypeName()
    {
        var type = typeof(T);

        return type.IsGenericType ?
            $"{type.Name}-{type.GetGenericArguments().FirstOrDefault()?.Name}" :
            type.Name;
    }
}
