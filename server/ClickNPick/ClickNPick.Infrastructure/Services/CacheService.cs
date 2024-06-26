﻿using ClickNPick.Application.Abstractions.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ClickNPick.Infrastructure.Services;

public class CacheService : ICacheService
{
    private readonly bool _cacheEnabled;
    private readonly IMemoryCache _memoryCache;

    public CacheService(
        IMemoryCache memoryCache,
        IConfiguration configuration)
    {
        _memoryCache = memoryCache;
        _cacheEnabled = configuration.GetValue<bool>("CacheSettings:Enabled");
    }

    public T GetOrCreate<T>(string cacheKey, Func<T> cacheItem, TimeSpan expirationTime)
    {
        if (_cacheEnabled)
        {
            if (string.IsNullOrEmpty(cacheKey))
            {
                Log.Error("Key cannot be null or empty.");
                throw new ArgumentException("Key cannot be null or empty.", nameof(cacheKey));
            }

            if (cacheItem == null)
            {
                Log.Error("Value cannot be null.");
                throw new ArgumentNullException(nameof(cacheItem), "Value cannot be null.");
            }

            var cacheResult = _memoryCache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = expirationTime;
                Log.Information("Item created and added to cache with key: {0}", cacheKey);
                return cacheItem();
            });

            return cacheResult;
        }

        return cacheItem();
    }

    public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> cacheItem, TimeSpan expirationTime)
    {
        if (_cacheEnabled)
        {
            if (string.IsNullOrEmpty(cacheKey))
            {
                Log.Error("Key cannot be null or empty.");
                throw new ArgumentException("Key cannot be null or empty.", nameof(cacheKey));
            }

            if (cacheItem == null)
            {
                Log.Error("Value cannot be null.");
                throw new ArgumentNullException(nameof(cacheItem), "Value cannot be null.");
            }

            var cacheResult = await _memoryCache.GetOrCreateAsync(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = expirationTime;
                Log.Information("Item created and added to cache with key: {0}", cacheKey);
                return Task.FromResult(cacheItem());
            });

            return await cacheResult;
        }

        return await cacheItem();
    }

    public void Remove(string cacheKey)
    {
        if (_cacheEnabled)
        {
            if (string.IsNullOrEmpty(cacheKey))
            {
                Log.Error("Key cannot be null or empty.");
                throw new ArgumentException("Key cannot be null or empty.", nameof(cacheKey));
            }

            _memoryCache.Remove(cacheKey);

            Log.Information("Item removed from cache with key: {0}", cacheKey);
        }
    }

    public void RemoveMany(IEnumerable<string> cacheKeys)
    {
        if (_cacheEnabled)
        {
            foreach (var cacheKey in cacheKeys)
            {
                _memoryCache.Remove(cacheKey);
            }
        }
    }

    public void ClearEntireCache()
    {
        if (_cacheEnabled)
        {
            _memoryCache.Dispose();
            Log.Information("Cache cleared.");
        }
    }
}
