namespace ClickNPick.Application.Abstractions.Services;

public interface ICacheService
{
    T GetOrCreate<T>(string cacheKey, Func<T> cacheItem, TimeSpan expirationTime);

    Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> cacheItem, TimeSpan expirationTime);

    void Remove(string cacheKey);

    void RemoveMany(IEnumerable<string> cacheKeys);

    void ClearEntireCache();
}
