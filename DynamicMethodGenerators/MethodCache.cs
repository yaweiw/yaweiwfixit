using DynamicMethodGenerators;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace DynamicMethodHandleGenerators
{
    public static class MethodCache
    {
        private static ConcurrentDictionary<CacheKey, DynamicMethodHandle> methodCache = new ConcurrentDictionary<CacheKey, DynamicMethodHandle>();

        public static DynamicMethodHandle GetCachedMethodHandleByCacheKey(object obj, string method)
        {
            return GetCachedMethodHandleByCacheKey(obj, method, false, null);
        }

        public static DynamicMethodHandle GetCachedMethodHandleByCacheKey(object obj, string method, params object[] parameters)
        {
            return GetCachedMethodHandleByCacheKey(obj, method, true, parameters);
        }

        public static DynamicMethodHandle GetCachedMethodHandleByCacheKey(object obj, string method, bool hasParameters, params object[] parameters)
        {
            //todo: guard key is null
            var key = new CacheKey(obj.GetType().FullName, method, Utility.GetParameterTypes(hasParameters, parameters));
            DynamicMethodHandle mh = null;
            if (!methodCache.TryGetValue(key, out mh))
            {
                MethodInfo methodInfo = Utility.GetMethodByMethodName(obj.GetType(), method, hasParameters, parameters);
                mh = new DynamicMethodHandle(methodInfo, parameters);
                methodCache.TryAdd(key, mh);
            }
            return mh;
        }
    }
}
