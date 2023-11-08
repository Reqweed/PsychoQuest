using Application.Caches.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Repository.Contracts;

namespace Application.Caches.Implementations;

public class CacheManager<TEntity> : ICacheManager<TEntity>
{
    private readonly IMemoryCache _cache;
    public MemoryCacheEntryOptions CacheEntryOptions { get; set; } = new();
    private readonly ILoggerManager _loggerManager;
    
    public CacheManager(IMemoryCache cache, ILoggerManager loggerManager)
    {
        _cache = cache;
        _loggerManager = loggerManager;
    }

    public async Task<TEntity> GetOrSetCacheValue(object key, Func<Task<TEntity?>> query)
    {
        _loggerManager.LogInfo($"Cache: {key} has begun");
        
        if (!_cache.TryGetValue(key, out TEntity? entity))
        {
            _loggerManager.LogInfo($"Cache: {key} wasn't found");
            
            entity = await query();
            
            if (entity == null)
            {
                _loggerManager.LogInfo($"Cache: {key} - Query result is null");
                throw new Exception();
            }
            
            _cache.Set(key, entity, CacheEntryOptions);
            
            _loggerManager.LogInfo($"Cache: {key} was filled");
        }
        else
        {
            _loggerManager.LogInfo($"Cache: {key} was found");
        }

        if (entity == null)
        {
            _loggerManager.LogInfo($"Cache: {key} stores null");
            throw new Exception();
        }
        
        _loggerManager.LogInfo($"Cache: {key} was finished");
        
        return entity;
    }

    public void ChangeCacheValue(object key, TEntity newEntity)
    {
        if (!_cache.TryGetValue(key, out TEntity entity))
            return;
        
        if (entity == null)
            return;

        _cache.Set(key, newEntity, CacheEntryOptions);
    }

    public void RemoveCacheValue(object key)
    {
        if (_cache.TryGetValue(key, out TEntity _))
        {
            _cache.Remove(key);
        }
    }
}