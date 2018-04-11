using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.GraphQL.Helper.Schema;
using Backend.GraphQL.Helper.Schema.Base;
using Backend.GraphQL.Types;
using Backend.Repository;
using GraphQL.Types;

namespace Backend.GraphQL.Schema.Query
{
    [RegistrerQuery]
    public class PageQuery : GraphQLBase<PageGraphType, PageQuery.PageQueryExecutor>
    {
        public PageQuery(IServiceProvider serviceProvider) : base(serviceProvider, "Page")
        {
            this.Arguments.Add(new QueryArgument(typeof(StringGraphType))
            {
                Name = "url",
                Description = "The url of the page which is requested"
            });
        }

        public class PageQueryExecutor : IGraphQLExecutor
        {
            private readonly IPageRepository _pageRepository;

            public PageQueryExecutor(IPageRepository pageRepository)
            {
                _pageRepository = pageRepository;
            }

            public async Task<object> Resolve(ResolveFieldContext context)
            {
                var pageUrl = context.GetArgument<string>("url");
                if (string.IsNullOrWhiteSpace(pageUrl))
                {
                    throw new ArgumentException("url cannot be null or empty");
                }

                return await _pageRepository.GetPage(pageUrl).ConfigureAwait(false);
            }
        }
    }
}
