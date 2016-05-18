using DynamicMethodGenerators;
using DynamicMethodHandleGenerators;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace DynamicMethodHandleGenerators
{
    public class PropertyCache<T>
    {
        private static ConcurrentDictionary<CacheKey, DynamicPropertyHandle<T>> propertyCache = new ConcurrentDictionary<CacheKey, DynamicPropertyHandle<T>>();

        internal static DynamicPropertyHandle<T> GetCachedPropertyByCacheKey(string propertyName)
        {
            Type type = typeof(T);
            var key = new CacheKey(type.FullName, propertyName, Utility.GetParameterTypes(null));
            DynamicPropertyHandle<T> ph = null;
            if (!propertyCache.TryGetValue(key, out ph))
            {
                PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlagsConstants.propertyFlags);
                if (propertyInfo == null)
                    throw new InvalidOperationException(
                      string.Format("MemberNotFoundException {0}", propertyName));
                ph = new DynamicPropertyHandle<T>(propertyInfo);
                propertyCache.TryAdd(key, ph);
            }
            return ph;
        }
    }
}
