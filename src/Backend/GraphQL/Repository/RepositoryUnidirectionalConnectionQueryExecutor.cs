using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.GraphQL.Helper.Connections;
using Backend.Repository;
using GraphQL.Builders;
using GraphQL.Types.Relay.DataObjects;

namespace Backend.GraphQL.Repository
{
    /// <summary>
    /// Contains a UnidirectionalConnectionQueryExecutor which resolves from a <see cref="IGenericRepository{T}"/>
    /// </summary>
    /// <typeparam name="T">The type of the repository</typeparam>
    public class RepositoryUnidirectionalConnectionQueryExecutor<T> : UnidirectionalConnectionQueryExecutor
        where T : class
    {
        private readonly IGenericRepository<T> _repository;

        /// <summary>
        /// Constructs the executor with the specified repository as input parameter
        /// </summary>
        /// <param name="repository">The repository to resolve from</param>
        public RepositoryUnidirectionalConnectionQueryExecutor(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        protected override async Task<object> Resolve(ResolveConnectionContext<object> context)
        {
            // Get repository page info
            var repositoryPageInfo = await _repository.GetPageInfo(context.After, context.First ?? 10);

            // Get repository edges
            var repositoryEdges = await _repository.GetNodes(context.After, context.First ?? 10);

            // Get edges
            List<Edge<T>> edges = repositoryEdges.Select(e => new Edge<T>
            {
                Cursor = e.Cursor,
                Node = e.Node
            }).ToList();

            // Get page info
            PageInfo pageInfo = new PageInfo
            {
                StartCursor = repositoryPageInfo.StartCursor,
                EndCursor = repositoryPageInfo.EndCursor,
                HasNextPage = repositoryPageInfo.HasNextPage,
                HasPreviousPage = repositoryPageInfo.HasPrevPage
            };

            // Return connection
            return new Connection<T>
            {
                TotalCount = repositoryPageInfo.TotalCount,
                PageInfo = pageInfo,
                Edges = edges
            };
        }
    }
}
