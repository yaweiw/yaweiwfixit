using System;
using System.Linq;
using System.Reflection;

namespace DynamicMethodHandleGenerators
{
    public class DynamicMethodHandle
    {
        public string MethodName { get; private set; }
        public Func<object[], object> DynamicMethod { get; private set; }
        public bool HasFinalArrayParam { get; private set; }
        public int MethodParamsLength { get; private set; }
        public Type FinalArrayElementType { get; private set; }

        public DynamicMethodHandle(MethodInfo methodinfo, params object[] parameters)
        {
            if (methodinfo == null)
            {
                DynamicMethod = null;
            }
            else
            {
                MethodName = methodinfo.Name;
                var infoParams = methodinfo.GetParameters();
                object[] inParams = null;
                if (parameters == null)
                {
                    inParams = new object[] { null };

                }
                else
                {
                    inParams = parameters;
                }
                var pCount = infoParams.Length;
                if (pCount > 0 &&
                   ((pCount == 1 && infoParams[0].ParameterType.IsArray) ||
                   (infoParams[pCount - 1].GetCustomAttributes(typeof(ParamArrayAttribute), true).Length > 0)))
                {
                    HasFinalArrayParam = true;
                    MethodParamsLength = pCount;
                    FinalArrayElementType = infoParams[pCount - 1].ParameterType;
                }
                DynamicMethod = DynamicMethodHandleFactory.CreateMethod(methodinfo);
            }
        }
    }
}
