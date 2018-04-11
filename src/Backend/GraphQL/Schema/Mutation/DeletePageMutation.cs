using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.GraphQL.Helper.Schema;
using Backend.GraphQL.Helper.Schema.Base;
using Backend.GraphQL.Types;
using Backend.Repository;
using GraphQL.Types;

namespace Backend.GraphQL.Schema.Mutation
{
    [RegistrerMutation]
    public class DeletePageMutation : GraphQLBase<BooleanGraphType, DeletePageMutation.DeletePageMutationExecutor>
    {
        public DeletePageMutation(IServiceProvider serviceProvider) : base(serviceProvider, "deletePage")
        {
            this.Arguments.Add(new QueryArgument(typeof(StringGraphType))
            {
                Name = "url",
                Description = "The url of the page which should be deleted"
            });
        }

        public class DeletePageMutationExecutor : IGraphQLExecutor
        {
            private readonly IPageRepository _pageRepository;

            public DeletePageMutationExecutor(IPageRepository pageRepository)
            {
                _pageRepository = pageRepository;
            }

            public async Task<object> Resolve(ResolveFieldContext context)
            {
                // Get arguments
                var pageUrl = context.GetArgument<string>("url");
                if (string.IsNullOrWhiteSpace(pageUrl))
                {
                    throw new ArgumentException("url cannot be null or empty");
                }

                // Add page
                await _pageRepository.DeletePage(pageUrl).ConfigureAwait(false);

                // Return true
                return true;
            }
        }
    }
}
