using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Helpers
{
    /// <summary>
    /// Contains extention methods for resolving types using a <see cref="IServiceProvider"/>
    /// </summary>
    public static class ServiceProviderExtentions
    {
        /// <summary>
        /// Resolves the type <typeparamref name="T"/> using the <paramref name="serviceProvider"/>
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <param name="serviceProvider">The serviceProvider used to resolve dependencies</param>
        /// <returns>Returns the type <typeparamref name="T"/></returns>
        public static T Resolve<T>(this IServiceProvider serviceProvider)
        {
            return (T)Resolve(serviceProvider, typeof(T));
        }

        /// <summary>
        /// Resolves the type <paramref name="type"/> using the <paramref name="serviceProvider"/>
        /// </summary>
        /// <param name="serviceProvider">The serviceProvider used to resolve dependencies</param>
        /// <param name="type">The type to resolve</param>
        /// <returns>Returns the resolved instance of the type <paramref name="type"/></returns>
        public static object Resolve(this IServiceProvider serviceProvider, Type type)
        {
            var serviceType = serviceProvider.GetService(type);
            if (serviceType != null)
                return serviceType;

            // Check if constructor with 0 arguments was found
            if (type.GetConstructors().Any(e => e.IsPublic && e.GetParameters().Length == 0))
            {
                return Activator.CreateInstance(type);
            }

            // Get all constructors
            var constructors = type.GetConstructors().Where(e => e.IsPublic);
            foreach (var constructor in constructors)
            {
                var arguments = new List<object>();
                var foundAll = true;
                foreach (var argument in constructor.GetParameters())
                {
                    var resolvedArgument = Resolve(serviceProvider, argument.ParameterType);
                    if (resolvedArgument == null)
                    {
                        foundAll = false;
                        break;
                    }
                    arguments.Add(resolvedArgument);
                }

                if (foundAll)
                {
                    return Activator.CreateInstance(type, arguments.ToArray());
                }
            }

            throw new NotSupportedException("Cound not resolve constructor for type " + type);
        }
    }
}
