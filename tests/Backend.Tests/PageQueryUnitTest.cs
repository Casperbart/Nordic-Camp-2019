using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Repository;
using Backend.Repository.Mock.Repository;
using Backend.Tests.GraphQLMock;
using Microsoft.Extensions.DependencyInjection;
using SAHB.GraphQLClient.FieldBuilder.Attributes;
using SAHB.GraphQLClient.QueryGenerator;
using Xunit;

namespace Backend.Tests
{
    public class PageQueryUnitTest : BaseGraphQLUnitTest
    {
        private readonly Page _expectedPage1 = new Page { Url = "url_of_page", Content = "content_of_page" };
        private readonly Page _expectedPage2 = new Page { Url = "url_of_page2", Content = "content_of_page2" };

        [Fact]
        public async Task TestQuery1()
        {
            var queryOutput = await GraphQLClient
                .CreateQuery<PageQuery>("URL_NOT_USED",
                    arguments: new GraphQLQueryArgument("pageUrl", "Url_Of_Page"))
                .Execute();
            Assert.Equal(_expectedPage1.Url, queryOutput.Page.Url);
            Assert.Equal(_expectedPage1.Content, queryOutput.Page.Content);
        }

        [Fact]
        public async Task TestQuery2()
        {
            var queryOutput = await GraphQLClient
                .CreateQuery<PageQuery>("URL_NOT_USED", arguments: new GraphQLQueryArgument("pageUrl", "Url_Of_Page2"))
                .Execute();
            Assert.Equal(_expectedPage2.Url, queryOutput.Page.Url);
            Assert.Equal(_expectedPage2.Content, queryOutput.Page.Content);
        }

        protected override IServiceCollection RegistrerServices(IServiceCollection services)
        {
            return base.RegistrerServices(services)
                .AddSingleton<IPageRepository>(new PageRepository(_expectedPage1, _expectedPage2));
        }

        public class PageRepository : MockPageRepository, IPageRepository
        {
            private readonly Page[] _pages;

            public PageRepository(params Page[] pages)
            {
                _pages = pages;
            }

            public override Task<List<Page>> GetInitialData()
            {
                return Task.FromResult(_pages.ToList());
            }

            public override string GetCursor(Page item)
            {
                return item.Url;
            }
        }

        public class PageQuery
        {
            [GraphQLArguments("url", "String", "pageUrl")]
            public PageType Page { get; set; }
        }

        public class PageType
        {
            public string Url { get; set; }
            public string Content { get; set; }
        }
    }
}