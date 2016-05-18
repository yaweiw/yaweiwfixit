using DynamicMethodHandleGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMethodGenerators
{
    public static class Utility
    {

        public static Type[] GetParameterTypes(object[] parameters)
        {
            return GetParameterTypes(true, parameters);
        }

        public static Type[] GetParameterTypes(bool hasParameters, object[] parameters)
        {
            if (!hasParameters || parameters == null)
                return new Type[] { };

            List<Type> result = new List<Type>();

            foreach (object item in parameters)
            {
                if (item == null)
                {
                    result.Add(typeof(object));
                }
                else
                {
                    result.Add(item.GetType());
                }
            }
            return result.ToArray();
        }

        public static MethodInfo GetMethodByMethodName(Type type, string method, bool hasParameters, object[] parameters)
        {
            bool match = false;
            MethodInfo[] methodInfos = type.GetMethods(BindingFlagsConstants.commonMethodFlags);
            if (!hasParameters)
            {
                return Array.Find(methodInfos, mi => mi.Name == method);
            }

            // match based on method name, parameters length, type and order of parameters
            // expresive
            foreach (MethodInfo m in methodInfos)
            {
                if (m.Name == method && m.GetParameters().Length == parameters.Length)
                {
                    ParameterInfo[] parms = m.GetParameters();
                    for (int index = 0; index < parameters.Length; index++)
                    {
                        if (parameters[index].GetType().FullName != parms[index].ParameterType.FullName)
                        {
                            match = false;
                            break;
                        }
                        else
                        {
                            match = true;
                        }
                    }
                    if (match) return m;
                }
            }
            return null;
        }
    }
}
