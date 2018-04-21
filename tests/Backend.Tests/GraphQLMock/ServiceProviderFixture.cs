using System;
using Backend.GraphQL.Helper.Builder;
using Backend.GraphQL.Helper.Schema;
using GraphQL.DataLoader;
using GraphQL.Server.Transports.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Tests.GraphQLMock
{
    public class ServiceProviderFixture
    {
        public ServiceProviderFixture()
        {
            var services = new ServiceCollection();

            services.AddGraphQLHttp();
            services.RegistrerSchema<GraphQLQuery, GraphQLMutation>();
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddTransient<DataLoaderDocumentListener>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public IServiceProvider ServiceProvider { get; set; }
    }
}