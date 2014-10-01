using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using Fashion.Framework.Mvc.Security;
using MvcSiteMapProvider.Caching;

namespace Fashion.ERP.Web.Helpers.Caching
{
    public class UserCacheProvider<T> : ICacheProvider<T>
    {
        public UserCacheProvider(ObjectCache cache)
        {
            if (cache == null)
                throw new ArgumentNullException("cache");
            _cache = cache;
        }
        private readonly ObjectCache _cache;

        #region ICacheProvider<T> Members

        public event EventHandler<MicroCacheItemRemovedEventArgs<T>> ItemRemoved;

        public bool Contains(string key)
        {
            return _cache.Contains(key);
        }

        public LazyLock Get(string key)
        {
            return (LazyLock)_cache.Get(key);
        }

        public bool TryGetValue(string key, out LazyLock value)
        {
            value = Get(key);
            if (value != null)
            {
                return true;
            }
            return false;
        }

        public void Add(string key, LazyLock item, ICacheDetails cacheDetails)
        {
            var policy = new CacheItemPolicy();

            // Set timeout
            policy.Priority = CacheItemPriority.NotRemovable;
            if (IsTimespanSet(cacheDetails.AbsoluteCacheExpiration))
            {
                policy.AbsoluteExpiration = DateTimeOffset.Now.Add(cacheDetails.AbsoluteCacheExpiration);
            }
            else if (IsTimespanSet(cacheDetails.SlidingCacheExpiration))
            {
                policy.SlidingExpiration = cacheDetails.SlidingCacheExpiration;
            }

            // Add dependencies
            var dependencies = (IList<ChangeMonitor>)cacheDetails.CacheDependency.Dependency;
            if (dependencies != null)
            {
                foreach (var dependency in dependencies)
                {
                    policy.ChangeMonitors.Add(dependency);
                }
            }

            // Setting priority to not removable ensures an 
            // app pool recycle doesn't unload the item, but a timeout will.
            policy.Priority = CacheItemPriority.NotRemovable;

            // Setup callback
            policy.RemovedCallback = CacheItemRemoved;

            var userId = FashionSecurity.GetLoggedUserId();

            if (userId.HasValue)
                _cache.Add(string.Format("{0}{1}", key, userId), item, policy);
            else
                _cache.Add(key, item, policy);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        #endregion

        private bool IsTimespanSet(TimeSpan timeSpan)
        {
            return (!timeSpan.Equals(TimeSpan.MinValue));
        }

        private void CacheItemRemoved(CacheEntryRemovedArguments arguments)
        {
            var item = arguments.CacheItem;
            var args = new MicroCacheItemRemovedEventArgs<T>(item.Key, ((LazyLock)item.Value).Get<T>(null));
            OnCacheItemRemoved(args);
        }

        protected virtual void OnCacheItemRemoved(MicroCacheItemRemovedEventArgs<T> e)
        {
            if (ItemRemoved != null)
            {
                ItemRemoved(this, e);
            }
        }
    }
}