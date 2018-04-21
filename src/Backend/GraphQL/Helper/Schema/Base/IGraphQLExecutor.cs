using System.Threading.Tasks;
using GraphQL.Types;

namespace Backend.GraphQL.Helper.Schema.Base
{
    /// <summary>
    /// A GraphQL executor is used for resolving fields when a query is executed
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface IGraphQLExecutor
    {
        /// <summary>
        /// Resolves a field
        /// </summary>
        /// <param name="context">The <see cref="ResolveFieldContext"/> provided by GraphQL.NET</param>
        /// <returns>Returns the type which should be mapped from</returns>
        Task<object> Resolve(ResolveFieldContext context);
    }

    /// <summary>
    /// A GraphQL executor is used for resolving fields when a query is executed
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface IGraphQLExecutor<T>
    {
        /// <summary>
        /// Resolves a field
        /// </summary>
        /// <param name="context">The <see cref="ResolveFieldContext{TSource}"/> provided by GraphQL.NET</param>
        /// <returns>Returns the type which should be mapped from</returns>
        Task<object> Resolve(ResolveFieldContext<T> context);
    }
}
