using System.Threading.Tasks;
using Backend.GraphQL.Helper.Schema.Base;
using Backend.Tests.GraphQLMock;
using Xunit;

namespace Backend.Tests
{
    public class HelloQueryUnitTest : BaseGraphQLUnitTest
    {
        [Fact]
        public async Task TestQuery()
        {
            var queryOutput = await GraphQLClient.CreateQuery<HelloQuery>("URL_NOT_USED").Execute();
            Assert.Equal("query", queryOutput.Hello);
        }

        public class HelloQuery
        {
            public string Hello { get; set; }
        }
    }
}
