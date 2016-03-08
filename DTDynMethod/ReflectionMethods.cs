namespace DTDynMethod
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Microsoft.WindowsAzure.Storage.Table;

    internal static class ReflectionMethods
    {
        #region IDictionary

        public static readonly MethodInfo IDictionary_SetItem =
            typeof(IDictionary<string, EntityProperty>).GetMethod("set_Item");

        public static readonly MethodInfo IDictionary_TryGetValue =
            typeof(IDictionary<string, EntityProperty>).GetMethod("TryGetValue");
       
        #endregion

        #region EntityProperty_Ctor
        
        public static readonly ConstructorInfo EntityProperty_Ctor_Boolean =
            typeof(EntityProperty).GetConstructor(new[] { typeof(bool?) });
        
        public static readonly ConstructorInfo EntityProperty_Ctor_Bytes =
            typeof(EntityProperty).GetConstructor(new[] { typeof(byte[]) });
        
        public static readonly ConstructorInfo EntityProperty_Ctor_DateTime =
            typeof(EntityProperty).GetConstructor(new[] { typeof(DateTime?) });
        
        public static readonly ConstructorInfo EntityProperty_Ctor_DateTimeOffset =
            typeof(EntityProperty).GetConstructor(new[] { typeof(DateTimeOffset?) });
        
        public static readonly ConstructorInfo EntityProperty_Ctor_Double =
            typeof(EntityProperty).GetConstructor(new[] { typeof(double?) });
        
        public static readonly ConstructorInfo EntityProperty_Ctor_Guid =
            typeof(EntityProperty).GetConstructor(new[] { typeof(Guid?) });
        
        public static readonly ConstructorInfo EntityProperty_Ctor_Integer =
            typeof(EntityProperty).GetConstructor(new[] { typeof(int?) });
        
        public static readonly ConstructorInfo EntityProperty_Ctor_Long =
            typeof(EntityProperty).GetConstructor(new[] { typeof(long?) });
        
        public static readonly ConstructorInfo EntityProperty_Ctor_String =
            typeof(EntityProperty).GetConstructor(new[] { typeof(string) });
       
        #endregion

        #region EntityProperty_GetValue
        
        //// public static readonly MethodInfo EntityProperty_Get_PropertyType =
        ////    typeof(EntityProperty).GetMethod("get_PropertyType");
        
        public static readonly MethodInfo EntityProperty_Get_BinaryValue =
            typeof(EntityProperty).GetMethod("get_BinaryValue");
        
        public static readonly MethodInfo EntityProperty_Get_BooleanValue =
            typeof(EntityProperty).GetMethod("get_BooleanValue");
       
        public static readonly MethodInfo EntityProperty_Get_DateTimeOffsetValue =
            typeof(EntityProperty).GetMethod("get_DateTimeOffsetValue");
     
        public static readonly MethodInfo EntityProperty_Get_DoubleValue =
            typeof(EntityProperty).GetMethod("get_DoubleValue");
     
        public static readonly MethodInfo EntityProperty_Get_GuidValue =
            typeof(EntityProperty).GetMethod("get_GuidValue");
    
        public static readonly MethodInfo EntityProperty_Get_Int32Value =
            typeof(EntityProperty).GetMethod("get_Int32Value");
      
        public static readonly MethodInfo EntityProperty_Get_Int64Value =
            typeof(EntityProperty).GetMethod("get_Int64Value");
      
        public static readonly MethodInfo EntityProperty_Get_StringValue =
            typeof(EntityProperty).GetMethod("get_StringValue");
       
        #endregion

        #region Nullable_Ctor

        public static readonly ConstructorInfo Nullable_Ctor_Boolean =
            typeof(bool?).GetConstructor(new[] { typeof(bool) });

        public static readonly ConstructorInfo Nullable_Ctor_DateTime =
            typeof(DateTime?).GetConstructor(new[] { typeof(DateTime) });
       
        public static readonly ConstructorInfo Nullable_Ctor_DateTimeOffset =
            typeof(DateTimeOffset?).GetConstructor(new[] { typeof(DateTimeOffset) });
        
        public static readonly ConstructorInfo Nullable_Ctor_Double =
            typeof(double?).GetConstructor(new[] { typeof(double) });
       
        public static readonly ConstructorInfo Nullable_Ctor_Guid =
            typeof(Guid?).GetConstructor(new[] { typeof(Guid) });
        
        public static readonly ConstructorInfo Nullable_Ctor_Integer =
            typeof(int?).GetConstructor(new[] { typeof(int) });
       
        public static readonly ConstructorInfo Nullable_Ctor_Long =
            typeof(long?).GetConstructor(new[] { typeof(long) });
        
        #endregion

        #region Nullable_Value
        
        public static readonly MethodInfo Nullable_Value_Boolean =
            typeof(bool?).GetMethod("get_Value");
        
        public static readonly MethodInfo Nullable_Value_DateTimeOffset =
            typeof(DateTimeOffset?).GetMethod("get_Value");
       
        public static readonly MethodInfo Nullable_Value_Double =
            typeof(double?).GetMethod("get_Value");
       
        public static readonly MethodInfo Nullable_Value_Guid =
            typeof(Guid?).GetMethod("get_Value");
      
        public static readonly MethodInfo Nullable_Value_Int =
            typeof(int?).GetMethod("get_Value");
      
        public static readonly MethodInfo Nullable_Value_Long =
            typeof(long?).GetMethod("get_Value");
       
        #endregion

        #region Nullable_HasValue
      
        public static readonly MethodInfo Nullable_HasValue_DateTimeOffset =
            typeof(DateTimeOffset?).GetMethod("get_HasValue");
      
        #endregion

        #region DateTimeOffset
        
        public static readonly MethodInfo DateTimeOffset_Get_UtcDateTime =
            typeof(DateTimeOffset).GetMethod("get_UtcDateTime");
      
        #endregion
    }
}
