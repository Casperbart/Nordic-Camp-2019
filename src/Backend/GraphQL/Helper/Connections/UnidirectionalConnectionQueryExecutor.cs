using System;
using System.Threading.Tasks;
using Backend.GraphQL.Helper.Schema.Base;
using GraphQL.Builders;
using GraphQL.Types;

namespace Backend.GraphQL.Helper.Connections
{
    /// <summary>
    /// Contains a abstract unidirectional connection executor used to resolve connections
    /// </summary>
    public abstract class UnidirectionalConnectionQueryExecutor : IGraphQLExecutor
    {
        /// <summary>
        /// The default page size, this can be overwritten
        /// </summary>
        protected int PageSize { get; set; } = 10;

        /// <inheritdoc />
        public Task<object> Resolve(ResolveFieldContext context)
        {
            var args = new ResolveConnectionContext<object>(context, true, PageSize);
            CheckForErrors(args);

            return Resolve(args);
        }

        /// <summary>
        /// Resolves the connection by providing the <see cref="ResolveConnectionContext{T}"/>
        /// </summary>
        /// <param name="context">The Connection context which contains information of the cursors</param>
        /// <returns>Returns the result from executing the field</returns>
        protected abstract Task<object> Resolve(ResolveConnectionContext<object> context);

        /// <summary>
        /// Check the connection context from errors
        /// </summary>
        /// <param name="args">The connection context given from the user which should be validated</param>
        protected void CheckForErrors(ResolveConnectionContext<object> args)
        {
            if (args.First.HasValue && args.Before != null)
            {
                throw new ArgumentException("Cannot specify both `first` and `before`.");
            }
            if (args.Last.HasValue && args.After != null)
            {
                throw new ArgumentException("Cannot specify both `last` and `after`.");
            }
            if (args.Before != null && args.After != null)
            {
                throw new ArgumentException("Cannot specify both `before` and `after`.");
            }
            if (args.First.HasValue && args.Last.HasValue)
            {
                throw new ArgumentException("Cannot specify both `first` and `last`.");
            }
            if (args.IsUnidirectional && (args.Last.HasValue || args.Before != null))
            {
                throw new ArgumentException("Cannot use `last` and `before` with unidirectional connections.");
            }
            if (args.IsPartial && args.NumberOfSkippedEntries.HasValue)
            {
                throw new ArgumentException("Cannot specify `numberOfSkippedEntries` with partial connection resolvers.");
            }
        }
    }
}
