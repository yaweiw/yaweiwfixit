using System;
using System.Reflection;

namespace DynamicMethodHandleGenerators
{
    internal class DynamicPropertyHandle<T>
  {
    public string PropertyName { get; private set; }
    public Type PropertyType { get; private set; }
    public Func<T, object> DynamicPropertyGet { get; private set; }
    public Action<T, object> DynamicPropertySet { get; private set; }

    //public string MemberFullName
    //{
    //  get { return MemberType + "." + MemberName; }
    //}

    public DynamicPropertyHandle(string propertyName, Type propertyType, Func<T, object> dynamicPropertyGet, Action<T, object> dynamicPropertySet)
    {
            PropertyName = propertyName;
            PropertyType = propertyType;
            DynamicPropertyGet = dynamicPropertyGet;
            DynamicPropertySet = dynamicPropertySet;
    }

    public DynamicPropertyHandle(PropertyInfo info) :
      this(
            info.Name,
            info.PropertyType,
            DynamicMethodHandleFactory.CreatePropertyGetter<T>(info),
            DynamicMethodHandleFactory.CreatePropertySetter<T>(info))
    { }
  }
}
