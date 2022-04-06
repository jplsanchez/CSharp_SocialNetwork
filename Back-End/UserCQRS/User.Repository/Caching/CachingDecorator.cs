using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using User.Domain.Interfaces.Repositories;
using User.Domain.Models.Base;

namespace User.Repository.Caching
{
    public class CachingDecorator<T> : IReadRepository<T> where T : BaseModel
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IReadRepository<T> _inner;
        private readonly ILogger<CachingDecorator<T>> _logger;

        public CachingDecorator(IMemoryCache memoryCache, IReadRepository<T> inner, ILogger<CachingDecorator<T>> logger)
        {
            _memoryCache = memoryCache;
            _inner = inner;
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var key = nameof(T);
            var items = _memoryCache.Get<IEnumerable<T>>(key);
            if (items == null)
            {
                items = await _inner.GetAll();
                _logger.LogTrace("Cache miss for {Cachekey}", key);
                if (items != null)
                {
                    _logger.LogTrace("Settings items in cache for {CacheKey}", key);
                    _memoryCache.Set(key, items, TimeSpan.FromMinutes(1));
                }
            }
            else
            {
                _logger.LogTrace("Cache hit for {CacheKey}", key);
            }
            return items;
        }

        public async Task<T> Get(Guid id)
        {
            var key = $"{nameof(T)}:{id}";
            var items = _memoryCache.Get<T>(key);
            if (items == null)
            {
                items = await _inner.Get(id);
                _logger.LogTrace("Cache miss for {Cachekey}", key);
                if (items != null)
                {
                    _logger.LogTrace("Settings items in cache for {CacheKey}", key);
                    _memoryCache.Set(key, items, TimeSpan.FromMinutes(1));
                }
            }
            else
            {
                _logger.LogTrace("Cache hit for {CacheKey}", key);
            }
            return items;
        }
    }
}