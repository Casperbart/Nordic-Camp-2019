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
    public class CreatePageMutation : GraphQLBase<PageGraphType, CreatePageMutation.CreatePageMutationExecutor>
    {
        public CreatePageMutation(IServiceProvider serviceProvider) : base(serviceProvider, "createPage")
        {
            this.Arguments.Add(new QueryArgument(typeof(StringGraphType))
            {
                Name = "url",
                Description = "The url of the page which should be created"
            });

            this.Arguments.Add(new QueryArgument(typeof(StringGraphType))
            {
                Name = "content",
                Description = "The content of the page which should be created"
            });
        }

        public class CreatePageMutationExecutor : IGraphQLExecutor
        {
            private readonly IPageRepository _pageRepository;

            public CreatePageMutationExecutor(IPageRepository pageRepository)
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
                return await _pageRepository.AddPage(pageUrl, pageContent).ConfigureAwait(false);
            }
        }
    }
}
