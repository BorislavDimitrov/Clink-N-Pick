namespace ClickNPick.Application.Configurations.Cache;

public interface ICacheable
{
    IEnumerable<CacheParameter> GetCacheParameters();
}
