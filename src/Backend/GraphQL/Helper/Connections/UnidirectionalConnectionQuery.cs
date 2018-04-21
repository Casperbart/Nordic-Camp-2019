using System;
using Backend.GraphQL.Helper.Schema.Base;
using GraphQL.Types;
using GraphQL.Types.Relay;

namespace Backend.GraphQL.Helper.Connections
{
    /// <summary>
    /// Represents a GraphQL unidirectional connection query
    /// </summary>
    /// <typeparam name="T">The type which is mapped to a GraphQL type</typeparam>
    /// <typeparam name="TExecutor">The GraphQL Executor which resolves the result</typeparam>
    public abstract class UnidirectionalConnectionQuery<T, TExecutor> : GraphQLBase<ConnectionType<T>, TExecutor>
        where T : IGraphType
        where TExecutor : UnidirectionalConnectionQueryExecutor
    {
        /// <summary>
        /// Constructs a GraphQL unidirectional connection field
        /// </summary>
        /// <param name="serviceProvider">The serviceProvider used to resolve the <typeparamref name="TExecutor"/></param>
        /// <param name="name">The name of the GraphQL Field</param>
        protected UnidirectionalConnectionQuery(IServiceProvider serviceProvider, string name) : base(serviceProvider, name)
        {
            this.Arguments.Add(new QueryArgument(typeof(StringGraphType))
            {
                Name = "after",
                Description = "Only look at connected edges with cursors greater than the value of `after`."
            });
            this.Arguments.Add(new QueryArgument(typeof(IntGraphType))
            {
                Name = "first",
                Description =
                    "Specifies the number of edges to return starting from `after` or the first entry if `after` is not specified."
            });
        }
    }
}