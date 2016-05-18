﻿using System;

namespace DynamicMethodHandleGenerators
{
    internal class CacheKey
    {
        public string TypeName { get; private set; }
        public string MethodName { get; private set; }
        public Type[] ParamTypes { get; private set; }
        private int _hashKey;

        public CacheKey(string typeName, string methodName, Type[] paramTypes)
        {
            TypeName = typeName;
            MethodName = methodName;
            ParamTypes = paramTypes;

            _hashKey = typeName.GetHashCode();
            _hashKey = _hashKey ^ methodName.GetHashCode();
            foreach (Type item in paramTypes)
                _hashKey = _hashKey ^ item.Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            CacheKey key = obj as CacheKey;
            if (key != null &&
                key.TypeName == TypeName &&
                key.MethodName == MethodName &&
                ArrayEquals(key.ParamTypes, ParamTypes))
                return true;

            return false;
        }

        private bool ArrayEquals(Type[] a1, Type[] a2)
        {
            if (a1.Length != a2.Length)
                return false;

            for (int pos = 0; pos < a1.Length; pos++)
                if (a1[pos] != a2[pos])
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            return _hashKey;
        }
    }
}