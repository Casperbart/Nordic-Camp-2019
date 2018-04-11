using System;
using System.Threading.Tasks;
using GraphQL.Resolvers;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.GraphQL.Helper.Schema.Base
{
    public abstract class GraphQLBase<TGraphType, TQueryExecutor> : GraphQLBaseInformation
        where TGraphType : IGraphType
        where TQueryExecutor : IGraphQLExecutor
    {
        // If the order of the typeparameters is changed, remember to change the builder as well
        private readonly IServiceProvider _serviceProvider;

        public GraphQLBase(IServiceProvider serviceProvider, string name)
        {
            _serviceProvider = serviceProvider;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = typeof(TGraphType);
            Resolver = new FuncFieldResolver<object>(Resolve);
        }

        protected virtual async Task<object> Resolve(ResolveFieldContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                IGraphQLExecutor graphQlExecutor = scope.ServiceProvider.GetService<TQueryExecutor>();
                return await graphQlExecutor.Resolve(context);
            }
        }
    }
}