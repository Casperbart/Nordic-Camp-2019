using System;
using System.Threading.Tasks;
using Backend.GraphQL.Helper.Builder;
using Backend.Helpers;
using GraphQL.Resolvers;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.GraphQL.Helper.Schema.Base
{
    /// <summary>
    /// Contains a base GraphQL field which is automatically registred using the method <see cref="GraphQLServiceBuilder.RegistrerSchema{TQuery,TMutation}"/> and supporting executing the field with a <see cref="IGraphQLExecutor"/>
    /// </summary>
    /// <typeparam name="TGraphType">The GraphType which should be registred</typeparam>
    /// <typeparam name="TQueryExecutor">The query executor used to execute the GraphQL field</typeparam>
    public abstract class GraphQLBase<TGraphType, TQueryExecutor> : GraphQLBaseInformation
        where TGraphType : IGraphType
        where TQueryExecutor : IGraphQLExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Creates a new GraphQL field with the specified name as specified in <paramref name="name"/>
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> used to resolve the <typeparamref name="TQueryExecutor"/></param>
        /// <param name="name">The name of the GraphQL field</param>
        public GraphQLBase(IServiceProvider serviceProvider, string name)
        {
            _serviceProvider = serviceProvider;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = typeof(TGraphType);
            Resolver = new FuncFieldResolver<object>(Resolve);
            Arguments = new QueryArguments();
        }

        /// <summary>
        /// Resolves the field using the <typeparamref name="TQueryExecutor"/>
        /// </summary>
        /// <param name="context">The field context</param>
        /// <returns>Returns the object which should be mapped to the GraphQL field</returns>
        protected virtual Task<object> Resolve(ResolveFieldContext context)
        {
            return _serviceProvider.ResolveUsingExecutor<TGraphType, TQueryExecutor>(context);
        }
    }
}