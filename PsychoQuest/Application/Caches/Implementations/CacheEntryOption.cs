using Microsoft.Extensions.Caching.Memory;

namespace Application.Caches.Implementations;

public static class CacheEntryOption
{
    public static readonly MemoryCacheEntryOptions DefaultCacheEntry = GetDefaultCacheEntry;

    private static MemoryCacheEntryOptions GetDefaultCacheEntry =>
        new MemoryCacheEntryOptions().SetSize(1).SetSlidingExpiration(TimeSpan.FromHours(3)); 
}