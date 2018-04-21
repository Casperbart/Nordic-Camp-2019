using System;
using Backend.GraphQL.Helper.Schema;
using GraphQL;
using Microsoft.Extensions.DependencyInjection;
using SAHB.GraphQLClient;
using SAHB.GraphQLClient.FieldBuilder;
using SAHB.GraphQLClient.QueryGenerator;
using Xunit;

namespace Backend.Tests.GraphQLMock
{
    public class BaseGraphQLUnitTest : IClassFixture<ServiceProviderFixture>, IDisposable
    {
        private readonly IServiceScope _serviceScope;
        
        public BaseGraphQLUnitTest(ServiceProviderFixture serviceProvider)
        {
            _serviceScope = serviceProvider.ServiceProvider.CreateScope();
            Services = _serviceScope.ServiceProvider;


            Schema = Services.GetRequiredService<GraphQLSchema<GraphQLQuery, GraphQLMutation>>();
            GraphQLClient =
                new GraphQLHttpClient(new GraphQLExecutor(Schema, Services.GetRequiredService<IDocumentExecuter>()),
                    new GraphQLFieldBuilder(), new GraphQLQueryGeneratorFromFields());
        }

        protected IServiceProvider Services { get; }

        protected GraphQLSchema<GraphQLQuery, GraphQLMutation> Schema { get; }

        protected IGraphQLHttpClient GraphQLClient { get; }

        public void Dispose()
        {
            _serviceScope.Dispose();
        }
    }
}