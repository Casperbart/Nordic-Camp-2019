using System;
using System.Threading.Tasks;
using Backend.GraphQL.Helper.Schema.Base;
using Backend.Helpers;
using GraphQL.Builders;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.GraphQL.Helper
{
    /// <summary>
    /// Contains extention methods for working with <see cref="IGraphQLExecutor"/> and <see cref="IGraphQLExecutor{T}"/>
    /// </summary>
    public static class GraphQLResolverExtentions
    {
        /// <summary>
        /// Resolve the field using the <typeparamref name="TQueryExecutor"/>
        /// </summary>
        /// <typeparam name="TSource">The GraphQL source type</typeparam>
        /// <typeparam name="TQueryExecutor">The query executor used to resolve</typeparam>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> used to resolve the <typeparamref name="TQueryExecutor"/></param>
        /// <param name="resolveFieldContext">The <see cref="ResolveFieldContext"/> used to resolve the output from the field</param>
        /// <returns>Returns the resolved object</returns>
        public static async Task<object> ResolveUsingExecutor<TSource, TQueryExecutor>(this IServiceProvider serviceProvider, ResolveFieldContext resolveFieldContext)
            where TQueryExecutor : IGraphQLExecutor
        {
            using (var scope = serviceProvider.CreateScope())
            {
                TQueryExecutor graphQlExecutor = scope.ServiceProvider.Resolve<TQueryExecutor>();
                return await graphQlExecutor.Resolve(resolveFieldContext).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Resolve the field using the <typeparamref name="TQueryExecutor"/>
        /// </summary>
        /// <typeparam name="TSource">The GraphQL source type</typeparam>
        /// <typeparam name="TQueryExecutor">The query executor used to resolve</typeparam>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> used to resolve the <typeparamref name="TQueryExecutor"/></param>
        /// <param name="resolveFieldContext">The <see cref="ResolveFieldContext{TSource}"/> used to resolve the output from the field</param>
        /// <returns>Returns the resolved object</returns>
        public static async Task<object> ResolveUsingExecutor<TSource, TQueryExecutor>(this IServiceProvider serviceProvider, ResolveFieldContext<TSource> resolveFieldContext)
            where TQueryExecutor : IGraphQLExecutor<TSource>
        {
            using (var scope = serviceProvider.CreateScope())
            {
                TQueryExecutor graphQlExecutor = scope.ServiceProvider.Resolve<TQueryExecutor>();
                return await graphQlExecutor.Resolve(resolveFieldContext).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Resolve the field using the <typeparamref name="TQueryExecutor"/>
        /// </summary>
        /// <typeparam name="TSource">The GraphQL source type</typeparam>
        /// <typeparam name="TQueryExecutor">The query executor used to resolve</typeparam>
        /// <param name="fieldBuilder">The fieldBuilder which is used to build the field</param>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> used to resolve the <typeparamref name="TQueryExecutor"/></param>
        /// <returns>The same field builder which is used to build the field</returns>
        public static FieldBuilder<TSource, object> ResolveUsingExecutor<TSource, TQueryExecutor>(
            this FieldBuilder<TSource, object> fieldBuilder, IServiceProvider serviceProvider)
            where TQueryExecutor : IGraphQLExecutor<TSource>
        {
            return fieldBuilder.ResolveAsync(serviceProvider.ResolveUsingExecutor<TSource, TQueryExecutor>);
        }
    }
}
