using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;
using System.Collections.Concurrent;

namespace DynamicMethodHandleGenerators
{
    /// <summary>
    /// Provides methods to dynamically find and call methods.
    /// </summary>
    public static class DynamicMethodCaller<T> where T :class
    {
        public static object CallPropertyGetter(T target, string property)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (string.IsNullOrEmpty(property))
                throw new ArgumentException("Argument is null or empty.", "property");
            var ph = PropertyCache<T>.GetCachedPropertyByCacheKey(property);
            if (ph.DynamicPropertyGet == null)
            {
                throw new NotSupportedException(string.Format(
                  CultureInfo.CurrentCulture,
                  "The property '{0}' on Type '{1}' does not have a public getter.",
                  property,
                  target.GetType()));
            }

            return ph.DynamicPropertyGet(target);
        }

        public static void CallPropertySetter(T target, string property, object value)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (string.IsNullOrEmpty(property))
                throw new ArgumentException("Argument is null or empty.", "property");
            var ph = PropertyCache<T>.GetCachedPropertyByCacheKey(property);
            if (ph.DynamicPropertySet == null)
            {
                throw new NotSupportedException(string.Format(
                  CultureInfo.CurrentCulture,
                  "The property '{0}' on Type '{1}' does not have a public setter.",
                  property,
                  target.GetType()));
            }
            ph.DynamicPropertySet(target, value);
        }


        public static object CallMethodIfImplemented(T target, string method)
        {
            return CallMethodIfImplemented(target, method, false, null);
        }

        public static object CallMethodIfImplemented(T target, string method, params object[] parameters)
        {
            return CallMethodIfImplemented(target, method, true, parameters);
        }

        private static object CallMethodIfImplemented(T target, string method, bool hasParameters, params object[] parameters)
        {
            var mh = MethodCache.GetCachedMethodHandleByCacheKey(target, method, parameters);
            if (mh == null || mh.DynamicMethod == null) return null;
            return CallMethod(target, mh, hasParameters, parameters);
        }

        public static bool IsMethodImplemented(T target, string method, params object[] parameters)
        {
            var mh = MethodCache.GetCachedMethodHandleByCacheKey(target, method, parameters);
            return mh != null && mh.DynamicMethod != null;
        }

        public static object CallMethod(T target, string method)
        {
            return CallMethod(target, method, false, null);
        }

        public static object CallMethod(T target, string method, params object[] parameters)
        {
            return CallMethod(target, method, true, parameters);
        }

        private static object CallMethod(T target, string method, bool hasParameters, params object[] parameters)
        {
            var mh = MethodCache.GetCachedMethodHandleByCacheKey(target, method, hasParameters, parameters);
            if (mh == null || mh.DynamicMethod == null)
                throw new NotImplementedException(typeof(T) + "." + method + " " + "MethodNotImplemented");
            return CallMethod(target, mh, hasParameters, parameters);
        }

        public static object CallMethod(T target, MethodInfo info, params object[] parameters)
        {
            return CallMethod(target, info, true, parameters);
        }

        //private static object CallMethod(T target, MethodInfo info, bool hasParameters, params object[] parameters)
        //{
        //    var mh = MethodCache.GetCachedMethodHandleByCacheKey(target, info, parameters);
        //    if (mh == null || mh.DynamicMethod == null)
        //        throw new NotImplementedException(typeof(T) + "." + info.Name + " " + "MethodNotImplemented");
        //    return CallMethod(target, mh, hasParameters, parameters);
        //}

        private static object CallMethod(object obj, DynamicMethodHandle methodHandle, bool hasParameters, params object[] parameters)
        {
            object result = null;
            var method = methodHandle.DynamicMethod;

            object[] inParams = null;
            if (parameters == null)
                inParams = new object[] { null };
            else
                inParams = parameters;

            if (methodHandle.HasFinalArrayParam)
            {
                // last param is a param array or only param is an array
                var pCount = methodHandle.MethodParamsLength;
                var inCount = inParams.Length;
                if (inCount == pCount - 1)
                {
                    // no paramter was supplied for the param array
                    // copy items into new array with last entry null
                    object[] paramList = new object[pCount];
                    for (var pos = 0; pos <= pCount - 2; pos++)
                        paramList[pos] = parameters[pos];
                    paramList[paramList.Length - 1] = hasParameters && inParams.Length == 0 ? inParams : null;

                    // use new array
                    inParams = paramList;
                }
                else if ((inCount == pCount && inParams[inCount - 1] != null && !inParams[inCount - 1].GetType().IsArray) || inCount > pCount)
                {
                    // 1 or more params go in the param array
                    // copy extras into an array
                    var extras = inParams.Length - (pCount - 1);
                    object[] extraArray = GetExtrasArray(extras, methodHandle.FinalArrayElementType);
                    Array.Copy(inParams, pCount - 1, extraArray, 0, extras);

                    // copy items into new array
                    object[] paramList = new object[pCount];
                    for (var pos = 0; pos <= pCount - 2; pos++)
                        paramList[pos] = parameters[pos];
                    paramList[paramList.Length - 1] = extraArray;

                    // use new array
                    inParams = paramList;
                }
            }
            try
            {
                result = methodHandle.DynamicMethod(obj, inParams);
            }
            catch (Exception ex)
            {
                throw new Exception(obj.GetType().Name + "." + methodHandle.MethodName + " " + "MethodCallFailed");
            }
            return result;
        }

        private static object[] GetExtrasArray(int count, Type arrayType)
        {
            return (object[])(System.Array.CreateInstance(arrayType.GetElementType(), count));
        }
    }
}
