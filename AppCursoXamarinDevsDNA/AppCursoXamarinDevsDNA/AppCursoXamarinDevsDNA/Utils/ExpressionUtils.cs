using System;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace AppCursoXamarinDevsDNA.Utils
{
    public static class ExpressionUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public delegate object ObjectActivator();

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Instance<T>()
        {
            try
            {
                return (T)Instance(typeof(T));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return default(T);
            }

        }

        /// <summary>
        /// Instances the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object Instance(Type type)
        {
            var created = ExpressionUtils.GetActivator(type);
            return created();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static ObjectActivator GetActivator(Type type)
        {
            return GetActivator(GetConstructrorInfo(type));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static ObjectActivator GetActivator<T>()
        {
            return GetActivator(GetConstructorInfo<T>());
        }

        private static ConstructorInfo GetConstructorInfo<T>()
        {
            return GetConstructrorInfo(typeof(T));
        }

        private static ConstructorInfo GetConstructrorInfo(Type type)
        {
            //Take only the ctor that don't have parameters. This method only use for ViewModels.
            return type.GetTypeInfo().DeclaredConstructors.Where(x => x.GetParameters().Length == 0).First();
        }
        private static ObjectActivator GetActivator(ConstructorInfo ctor)
        {
            Type type = ctor.DeclaringType;

            NewExpression ctorExpr = Expression.New(ctor);

            var lambda = Expression.Lambda(typeof(ObjectActivator), ctorExpr, null);

            ObjectActivator compiled = (ObjectActivator)lambda.Compile();

            return compiled;
        }
    }
}
