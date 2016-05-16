using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DynamicMethodHandlerFactoryNameSpace
{
    public static class DynamicMethodHandlerFactory<T1, T2, T3>
    {
        public static Func<T1,T3> CreateConstructor(ConstructorInfo constructor)
        {
            if (constructor == null)
                throw new ArgumentNullException("ConstructorInfo is null");
            if (constructor.GetParameters().Length > 0)
                throw new NotSupportedException("ConstructorsWithParametersNotSupported");

            Expression body = Expression.New(constructor);
            if (constructor.DeclaringType.IsValueType)
            {
                body = Expression.Convert(body, typeof(T1));
            }

            return Expression.Lambda<Func<T1,T3>>(body, Expression.Parameter(typeof(T1))).Compile();
        }

        public static Func<T1, T2, T3> CreateMethod(MethodInfo method)
        {
            if (method == null) throw new ArgumentNullException("method");

            ParameterInfo[] pi = method.GetParameters();
            var targetExpression = Expression.Parameter(typeof(T2));
            var parametersExpression = Expression.Parameter(typeof(T3));

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

            Expression instance = Expression.Convert(targetExpression, method.DeclaringType);
            Expression body = pi.Length > 0
              ? Expression.Call(instance, method, callParametrs)
              : Expression.Call(instance, method);

            if (method.ReturnType == typeof(void))
            {
                var target = Expression.Label(typeof(T2));
                var nullRef = Expression.Constant(null);
                body = Expression.Block(
                  body,
                   Expression.Return(target, nullRef),
                  Expression.Label(target, nullRef));
            }
            else if (method.ReturnType.IsValueType)
            {
                body = Expression.Convert(body, typeof(object));
            }

            var lambda = Expression.Lambda<Func<T1, T2, T3>>(
              body,
              targetExpression,
              parametersExpression);

            return lambda.Compile();
        }

    }
}
