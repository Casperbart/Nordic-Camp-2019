using System;
using Backend.GraphQL.Helper.Builder;
using Backend.GraphQL.Helper.Schema;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Server.Transports.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using SAHB.GraphQLClient;
using SAHB.GraphQLClient.FieldBuilder;
using SAHB.GraphQLClient.QueryGenerator;
using Xunit;

namespace Backend.Tests.GraphQLMock
{
    /// <summary>
    /// Contains base methods for helping with GraphQL unit tests
    /// </summary>
    public class BaseGraphQLUnitTest
    {
        /// <summary>
        /// Registrers services in the <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> which the services is registrered in</param>
        /// <returns>The <see cref="IServiceCollection"/> with the registrered services</returns>
        protected virtual IServiceCollection RegistrerServices(IServiceCollection services)
        {
            services.AddGraphQLHttp();
            services.RegistrerSchema<GraphQLQuery, GraphQLMutation>();
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddTransient<DataLoaderDocumentListener>();

            return services;
        }

        private IServiceProvider _services;
        private IGraphQLHttpClient _client;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> which can be used to resolve services
        /// </summary>
        protected IServiceProvider Services =>
            _services ?? (_services = RegistrerServices(new ServiceCollection()).BuildServiceProvider());
        
        /// <summary>
        /// Gets the GraphQL schema
        /// </summary>
        protected GraphQLSchema<GraphQLQuery, GraphQLMutation> Schema => Services.GetRequiredService<GraphQLSchema<GraphQLQuery, GraphQLMutation>>();

        /// <summary>
        /// Gets a GraphQL client which can execute against a GraphQL api
        /// </summary>
        protected IGraphQLHttpClient GraphQLClient =>
            _client ?? (_client = new GraphQLHttpClient(
                new GraphQLExecutor(Schema, Services.GetRequiredService<IDocumentExecuter>()),
                new GraphQLFieldBuilder(), new GraphQLQueryGeneratorFromFields()));
    }
}