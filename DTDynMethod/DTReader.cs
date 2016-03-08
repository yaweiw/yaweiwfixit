using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DTDynMethod
{
    public static class DTReader<T>
        where T : class, IDTReader, new()
    {
        /// <summary>
        /// reader, from IDictionary to object
        /// </summary>
        private static readonly Action<T, IDictionary<string, EntityProperty>> Reader;

        /// <summary>
        /// writer, from object to IDictionary
        /// </summary>
        private static readonly Action<T, IDictionary<string, EntityProperty>> Writer;

        static DTReader()
        {
            var reader = new DynamicMethod(GetReaderMethodName(), typeof(void), new Type[] { typeof(T), typeof(IDictionary<string, EntityProperty>) });
            var writer = new DynamicMethod(GetWriterMethodName(), typeof(void), new Type[] { typeof(T), typeof(IDictionary<string, EntityProperty>) });
            var readerIL = reader.GetILGenerator();
            var writerIL = writer.GetILGenerator();

            // EntityProperty temp;
            readerIL.DeclareLocal(typeof(EntityProperty));
            var locals = new Dictionary<Type, LocalBuilder>();

            foreach (var p in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                // exclude indexer
                if (p.GetIndexParameters().Length > 0)
                {
                    continue;
                }

                var getMethod = p.GetGetMethod(false);
                var setMethod = p.GetSetMethod(false);

                // exclude non-public-readwritable properties
                if (getMethod == null || setMethod == null)
                {
                    continue;
                }

                var name = p.Name;

                // except four wellknown properties
                if (name == "PartitionKey" || name == "RowKey" || name == "ETag" || name == "Timestamp")
                {
                    continue;
                }
                // reader: if (arg1.TryGetValue("<p.name>", out temp))
                var writeNextLabel = readerIL.DefineLabel();
                readerIL.Emit(OpCodes.Ldarg_1);
                readerIL.Emit(OpCodes.Ldstr, name);
                readerIL.Emit(OpCodes.Ldloca_S, (byte)0);
                readerIL.Emit(OpCodes.Callvirt, ReflectionMethods.IDictionary_TryGetValue);
                readerIL.Emit(OpCodes.Brfalse_S, writeNextLabel);

                // reader:     this.<p.name> = temp.Value;
                ReadPropertyValue(readerIL, setMethod, p, locals);
                readerIL.MarkLabel(writeNextLabel);

                // writer: arg1["<p.name>"] = new EntityProperty(this.<p.name>);
                writerIL.Emit(OpCodes.Ldarg_1);
                writerIL.Emit(OpCodes.Ldstr, name);
                WritePropertyValue(writerIL, getMethod, p);
                writerIL.Emit(OpCodes.Callvirt, ReflectionMethods.IDictionary_SetItem);
            }

            readerIL.Emit(OpCodes.Ret);
            writerIL.Emit(OpCodes.Ret);
            Reader = (Action<T, IDictionary<string, EntityProperty>>)reader.CreateDelegate(typeof(Action<T, IDictionary<string, EntityProperty>>));
            Writer = (Action<T, IDictionary<string, EntityProperty>>)writer.CreateDelegate(typeof(Action<T, IDictionary<string, EntityProperty>>));
        }

        private static void ReadPropertyValue(ILGenerator il, MethodInfo setMethod, PropertyInfo property, Dictionary<Type, LocalBuilder> locals)
        {
            Type pt = property.PropertyType;
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_0);

            // todo : check edmtype
            if (pt == typeof(string))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_StringValue);
            }
            else if (pt == typeof(byte[]))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_BinaryValue);
            }
            else if (pt == typeof(int?))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_Int32Value);
            }
            else if (pt == typeof(int))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_Int32Value);
                PopStructPushStructRef(il, locals, typeof(int?));
                il.Emit(OpCodes.Call, ReflectionMethods.Nullable_Value_Int);
            }
            else if (pt == typeof(long?))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_Int64Value);
            }
            else if (pt == typeof(long))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_Int64Value);
                PopStructPushStructRef(il, locals, typeof(long?));
                il.Emit(OpCodes.Call, ReflectionMethods.Nullable_Value_Long);
            }
            else if (pt == typeof(Guid?))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_GuidValue);
            }
            else if (pt == typeof(Guid))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_GuidValue);
                PopStructPushStructRef(il, locals, typeof(Guid?));
                il.Emit(OpCodes.Call, ReflectionMethods.Nullable_Value_Guid);
            }
            else if (pt == typeof(double?))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_DoubleValue);
            }
            else if (pt == typeof(double))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_DoubleValue);
                PopStructPushStructRef(il, locals, typeof(double?));
                il.Emit(OpCodes.Call, ReflectionMethods.Nullable_Value_Double);
            }
            else if (pt == typeof(DateTimeOffset?))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_DateTimeOffsetValue);
            }
            else if (pt == typeof(DateTimeOffset))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_DateTimeOffsetValue);
                PopStructPushStructRef(il, locals, typeof(DateTimeOffset?));
                il.Emit(OpCodes.Call, ReflectionMethods.Nullable_Value_DateTimeOffset);
            }
            else if (pt == typeof(DateTime?))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_DateTimeOffsetValue);
                PopStructPushStructRef(il, locals, typeof(DateTimeOffset?));
                il.Emit(OpCodes.Call, ReflectionMethods.Nullable_HasValue_DateTimeOffset);
                var nullValueLabel = il.DefineLabel();
                var commonLabel = il.DefineLabel();
                il.Emit(OpCodes.Brfalse_S, nullValueLabel);
                PushStructRef(il, locals, typeof(DateTimeOffset?));
                il.Emit(OpCodes.Call, ReflectionMethods.Nullable_Value_DateTimeOffset);
                PopStructPushStructRef(il, locals, typeof(DateTimeOffset));
                il.Emit(OpCodes.Call, ReflectionMethods.DateTimeOffset_Get_UtcDateTime);
                il.Emit(OpCodes.Newobj, ReflectionMethods.Nullable_Ctor_DateTime);
                il.Emit(OpCodes.Br_S, commonLabel);
                il.MarkLabel(nullValueLabel);
                PushStructRef(il, locals, typeof(DateTime?));
                il.Emit(OpCodes.Initobj, typeof(DateTime?));
                PushStruct(il, locals, typeof(DateTime?));
                il.MarkLabel(commonLabel);
            }
            else if (pt == typeof(DateTime))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_DateTimeOffsetValue);
                PopStructPushStructRef(il, locals, typeof(DateTimeOffset?));
                il.Emit(OpCodes.Call, ReflectionMethods.Nullable_Value_DateTimeOffset);
                PopStructPushStructRef(il, locals, typeof(DateTimeOffset));
                il.Emit(OpCodes.Call, ReflectionMethods.DateTimeOffset_Get_UtcDateTime);
            }
            else if (pt == typeof(bool?))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_BooleanValue);
            }
            else if (pt == typeof(bool))
            {
                il.Emit(OpCodes.Callvirt, ReflectionMethods.EntityProperty_Get_BooleanValue);
                PopStructPushStructRef(il, locals, typeof(bool?));
                il.Emit(OpCodes.Call, ReflectionMethods.Nullable_Value_Boolean);
            }
            else
            {
                throw new NotSupportedException(string.Format("The type({1}) of property ({0}) is not supported!", property.Name, pt));
            }
            il.Emit(OpCodes.Callvirt, setMethod);
        }

        private static void PushStructRef(ILGenerator il, Dictionary<Type, LocalBuilder> locals, Type type)
        {
            LocalBuilder local;
            if (!locals.TryGetValue(type, out local))
            {
                local = il.DeclareLocal(type);
                locals.Add(type, local);
            }
            il.Emit(OpCodes.Ldloca_S, local);
        }

        private static void PushStruct(ILGenerator il, Dictionary<Type, LocalBuilder> locals, Type type)
        {
            LocalBuilder local;
            if (!locals.TryGetValue(type, out local))
            {
                local = il.DeclareLocal(type);
                locals.Add(type, local);
            }
            il.Emit(OpCodes.Ldloc_S, local);
        }
        private static void PopStructPushStructRef(ILGenerator il, Dictionary<Type, LocalBuilder> locals, Type type)
        {
            LocalBuilder local;
            if (!locals.TryGetValue(type, out local))
            {
                local = il.DeclareLocal(type);
                locals.Add(type, local);
            }
            il.Emit(OpCodes.Stloc_S, local);
            il.Emit(OpCodes.Ldloca_S, local);
        }

        #region Writer
        private static void WritePropertyValue(ILGenerator il, MethodInfo getMethod, PropertyInfo property)
        {
            Type pt = property.PropertyType;
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Callvirt, getMethod);
            if (pt == typeof(string))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_String);
            }
            else if (pt == typeof(byte[]))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_Bytes);
            }
            else if (pt == typeof(int?))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_Integer);
            }
            else if (pt == typeof(int))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.Nullable_Ctor_Integer);
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_Integer);
            }
            else if (pt == typeof(long?))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_Long);
            }
            else if (pt == typeof(long))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.Nullable_Ctor_Long);
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_Long);
            }
            else if (pt == typeof(Guid?))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_Guid);
            }
            else if (pt == typeof(Guid))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.Nullable_Ctor_Guid);
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_Guid);
            }
            else if (pt == typeof(double?))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_Double);
            }
            else if (pt == typeof(double))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.Nullable_Ctor_Double);
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_Double);
            }
            else if (pt == typeof(DateTimeOffset?))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_DateTimeOffset);
            }
            else if (pt == typeof(DateTimeOffset))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.Nullable_Ctor_DateTimeOffset);
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_DateTimeOffset);
            }
            else if (pt == typeof(DateTime?))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_DateTime);
            }
            else if (pt == typeof(DateTime))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.Nullable_Ctor_DateTime);
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_DateTime);
            }
            else if (pt == typeof(bool?))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_Boolean);
            }
            else if (pt == typeof(bool))
            {
                il.Emit(OpCodes.Newobj, ReflectionMethods.Nullable_Ctor_Boolean);
                il.Emit(OpCodes.Newobj, ReflectionMethods.EntityProperty_Ctor_Boolean);
            }
            else
            {
                throw new NotSupportedException(string.Format("The type({1}) of property ({0}) is not supported!", property.Name, pt.FullName));
            }
        }
        #endregion  
        private static string GetReaderMethodName()
        {
            return "AzureTableEntityAdapter$" + typeof(T).FullName + "$Reader";
        }

        private static string GetWriterMethodName()
        {
            return "AzureTableEntityAdapter$" + typeof(T).FullName + "$Writer";
        }
    }
}
