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
    public class EditPageMutation : GraphQLBase<PageGraphType, EditPageMutation.EditPageMutationExecutor>
    {
        public EditPageMutation(IServiceProvider serviceProvider) : base(serviceProvider, "editPage")
        {
            this.Arguments.Add(new QueryArgument(typeof(StringGraphType))
            {
                Name = "url",
                Description = "The url of the page which should be edited"
            });

            this.Arguments.Add(new QueryArgument(typeof(StringGraphType))
            {
                Name = "content",
                Description = "The new content of the page"
            });
        }

        public class EditPageMutationExecutor : IGraphQLExecutor
        {
            private readonly IPageRepository _pageRepository;

            public EditPageMutationExecutor(IPageRepository pageRepository)
            {
                _pageRepository = pageRepository;
            }

            public async Task<object> Resolve(ResolveFieldContext context)
            {
                List<Exception> exceptions = new List<Exception>();

                // Get arguments
                var pageUrl = context.GetArgument<string>("url");
                if (string.IsNullOrWhiteSpace(pageUrl))
                {
                    exceptions.Add(new ArgumentException("url cannot be null or empty"));
                }
                var pageContent = context.GetArgument<string>("content");
                if (string.IsNullOrWhiteSpace(pageContent))
                {
                    exceptions.Add(new ArgumentException("content cannot be null or empty"));
                }

                // Throw exceptions if any
                if (exceptions.Any())
                    throw new AggregateException(exceptions);

                // Add page
                return await _pageRepository.EditPage(pageUrl, pageContent).ConfigureAwait(false);
            }
        }
    }
}
