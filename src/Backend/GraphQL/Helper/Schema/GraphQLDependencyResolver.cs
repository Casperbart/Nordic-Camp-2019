using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Helpers;
using GraphQL;

namespace Backend.GraphQL.Helper.Schema
{
    public class GraphQLDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public GraphQLDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            return _serviceProvider.Resolve(type);
        }
    }
}