using System;
using System.Threading.Tasks;
using Backend.GraphQL.Helper.Schema.Base;
using Backend.Helpers;
using GraphQL.Builders;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.GraphQL.Helper
{
    public static class GraphQLResolverExtentions
    {
        public static async Task<object> ResolveUsingExecutor<TSource, TQueryExecutor>(this IServiceProvider serviceProvider, ResolveFieldContext resolveFieldContext)
            where TQueryExecutor : IGraphQLExecutor
        {
            using (var scope = serviceProvider.CreateScope())
            {
                TQueryExecutor graphQlExecutor = scope.ServiceProvider.Resolve<TQueryExecutor>();
                return await graphQlExecutor.Resolve(resolveFieldContext).ConfigureAwait(false);
            }
        }

        public static async Task<object> ResolveUsingExecutor<TSource, TQueryExecutor>(this IServiceProvider serviceProvider, ResolveFieldContext<TSource> resolveFieldContext)
            where TQueryExecutor : IGraphQLExecutor<TSource>
        {
            using (var scope = serviceProvider.CreateScope())
            {
                TQueryExecutor graphQlExecutor = scope.ServiceProvider.Resolve<TQueryExecutor>();
                return await graphQlExecutor.Resolve(resolveFieldContext).ConfigureAwait(false);
            }
        }

        public static FieldBuilder<TSource, object> ResolveUsingExecutor<TSource, TQueryExecutor>(
            this FieldBuilder<TSource, object> fieldBuilder, IServiceProvider serviceProvider)
            where TQueryExecutor : IGraphQLExecutor<TSource>
        {
            return fieldBuilder.ResolveAsync(serviceProvider.ResolveUsingExecutor<TSource, TQueryExecutor>);
        }
    }
}
