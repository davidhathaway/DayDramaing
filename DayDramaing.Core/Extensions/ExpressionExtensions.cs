using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Innovations.Core.Extensions
{
    public static class ExpressionExtensions
    {
        public static string GetInputName<TModel, TProperty>(this Expression<Func<TModel, TProperty>> expression)
        {
            if (expression.Body.NodeType == ExpressionType.Call)
            {
                MethodCallExpression methodCallExpression = (MethodCallExpression)expression.Body;
                string name = GetInputName(methodCallExpression);
                return name.Substring(expression.Parameters[0].Name.Length + 1);

            }
            return expression.Body.ToString().Substring(expression.Parameters[0].Name.Length + 1);
        }

        public static MethodInfo GetMethod<TType, TSignature>(this TType type, Expression<Func<TType, TSignature>> methodSelector) where TType : class
        {
            var argument = ((MethodCallExpression)((UnaryExpression)methodSelector.Body).Operand).Arguments[2];
            return ((ConstantExpression)argument).Value as MethodInfo;
        }

        private static string GetInputName(this MethodCallExpression expression)
        {
            // p => p.Foo.Bar().Baz.ToString() => p.Foo OR throw...

            MethodCallExpression methodCallExpression = expression.Object as MethodCallExpression;
            if (methodCallExpression != null)
            {
                return GetInputName(methodCallExpression);
            }
            return expression.Object.ToString();
        }

        /// <summary>
        /// Get the property name from the expression.
        /// e.g. GetPropertyName(Person)( p => p.FirstName);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string GetPropertyName<T>(this Expression<Func<T, object>> exp)
        {
            MemberExpression memberExpression = null;

            // Get memberexpression.
            if (exp.Body is MemberExpression)
                memberExpression = exp.Body as MemberExpression;

            if (exp.Body is UnaryExpression)
            {
                var unaryExpression = exp.Body as UnaryExpression;
                if (unaryExpression.Operand is MemberExpression)
                    memberExpression = unaryExpression.Operand as MemberExpression;
            }

            if (memberExpression == null)
                throw new InvalidOperationException("Not a member access.");

            var info = memberExpression.Member as PropertyInfo;
            return info.Name;
        }

        /// <summary>
        /// Get the propertyinfo from the expression.
        /// e.g. GetPropertyInfo(Person)( p => p.FirstName);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, object>> exp, bool throwException = true)
        {
            MemberExpression memberExpression = null;

            // Get memberexpression.
            if (exp.Body is MemberExpression)
                memberExpression = exp.Body as MemberExpression;

            if (exp.Body is UnaryExpression)
            {
                var unaryExpression = exp.Body as UnaryExpression;
                if (unaryExpression.Operand is MemberExpression)
                    memberExpression = unaryExpression.Operand as MemberExpression;
            }

            if (memberExpression == null)
            {
                if (throwException)
                {
                    throw new InvalidOperationException("Not a member access.");
                }
                else
                {
                    return null;
                }
            }

            var info = memberExpression.Member as PropertyInfo;
            return info;
        }

    }
}
