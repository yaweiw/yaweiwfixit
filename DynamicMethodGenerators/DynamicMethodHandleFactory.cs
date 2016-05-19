using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace DynamicMethodHandleGenerators
{
    public static class DynamicMethodHandleFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="constructor"></param>
        /// <returns></returns>
        public static Func<object> CreateConstructor<T>(ConstructorInfo constructor)
        {
            if (constructor == null)
                throw new ArgumentNullException("constructor");
            if (constructor.GetParameters().Length > 0)
                throw new NotSupportedException("ConstructorsWithParametersNotSupported");

            Expression expBody = Expression.New(constructor);
            if (constructor.DeclaringType.IsValueType)
            {
                expBody = Expression.Convert(expBody, typeof(T));
            }

            return Expression.Lambda<Func<object>>(expBody).Compile();
        }
        public static Func<object, object[], object> CreateMethod(MethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            ParameterInfo[] pi = method.GetParameters();
            var targetExpression = Expression.Parameter(typeof(object));
            var parametersExpression = Expression.Parameter(typeof(object[]));

            Expression[] callParametrs = new Expression[pi.Length];
            for (int x = 0; x < pi.Length; x++)
            {
                callParametrs[x] =
                  Expression.Convert(
                    Expression.ArrayIndex(
                      parametersExpression,
                      Expression.Constant(x)),
                    pi[x].ParameterType);
            }

            // instance method
            Expression instance = Expression.Convert(targetExpression, method.DeclaringType);
            Expression callExp = pi.Length > 0
              ? Expression.Call(instance, method, callParametrs)
              : Expression.Call(instance, method);

            Expression body = null;
            LabelTarget returnTarget = Expression.Label(typeof(object));

            if (method.ReturnType == typeof(void))
            {
                Expression nullRef = Expression.Constant(null);
                body = Expression.Block(
                    callExp,
                    Expression.Return(returnTarget, nullRef));
            }
            else
            {
                //body = Expression.Block(
                //    callExp,
                //    Expression.Convert(callExp, typeof(object)));
                body = Expression.Convert(callExp, typeof(object));
            }

            //var result = Expression.Lambda<Func<object, object[], object>>(
            //  body,
            //  targetExpression,
            //  parametersExpression).Compile();

            //Console.WriteLine("The expressions from the block expression:");
            //foreach (var expr in body.Expressions)
            //    Console.WriteLine(expr.ToString());

            return Expression.Lambda<Func<object, object[], object>>(
              body,
              targetExpression,
              parametersExpression).Compile();
        }

        public static Func<T, object> CreatePropertyGetter<T>(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            if (!property.CanRead) return null;

            ParameterExpression target = Expression.Parameter(typeof(T), "targetinstance");
            MethodCallExpression expBody = Expression.Call(target, property.GetGetMethod());
            var parameters = new ParameterExpression[] { target };
            return Expression.Lambda<Func<T, object>>(expBody, parameters).Compile();
        }

        public static Action<T, object> CreatePropertySetter<T>(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            if (!property.CanWrite) return null;

            ParameterExpression target = Expression.Parameter(typeof(T));
            ParameterExpression val = Expression.Parameter(typeof(object));

            Expression expBody = Expression.Assign(
              Expression.Property(
                Expression.Convert(target, property.DeclaringType),
                property),
              Expression.Convert(val, property.PropertyType));

            return Expression.Lambda<Action<T, object>>(expBody, target, val).Compile();
        }
    }
}
