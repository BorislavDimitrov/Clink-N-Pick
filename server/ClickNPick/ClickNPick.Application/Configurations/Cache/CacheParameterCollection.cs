namespace ClickNPick.Application.Configurations.Cache;

public class CacheParameterCollection<T>
where T : ICacheable
{
    private IEnumerable<CacheParameter> cacheParameters;

    public CacheParameterCollection(params T[] cacheableObjects)
        => cacheParameters = new List<CacheParameter>(
            cacheableObjects
                .Where(_ => true)
                .SelectMany(x => x.GetCacheParameters()));

    public override string ToString()
        => string.Join(":", cacheParameters);
}
