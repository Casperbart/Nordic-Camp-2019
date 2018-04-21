using System;
using System.Linq;
using System.Reflection;
using Backend.GraphQL.Helper.Schema;
using Backend.GraphQL.Helper.Schema.Base;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.GraphQL.Helper.Builder
{
    /// <summary>
    /// Contains extention methods for registrering GraphQL schemas in DependencyInjection container
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class GraphQLServiceBuilder
    {
        /// <summary>
        /// Registrer a GraphQL schema using the specified <typeparamref name="TQuery"/> type and the specified <typeparamref name="TMutation"/> type
        /// </summary>
        /// <typeparam name="TQuery">The query GraphQL type</typeparam>
        /// <typeparam name="TMutation">The mutation GraphQL type</typeparam>
        /// <param name="services">The serviceCollection to registrer the schema on</param>
        /// <returns>The same <see cref="IServiceCollection"/> as recieved at parameter <paramref name="services"/></returns>
        public static IServiceCollection RegistrerSchema<TQuery, TMutation>(this IServiceCollection services)
            where TQuery : class, IObjectGraphType
            where TMutation : class, IObjectGraphType
        {
            // Registrer Schema, Query and Mutation
            services
                .AddSingleton<GraphQLSchema<TQuery, TMutation>>()
                .AddSingleton<TQuery>()
                .AddSingleton<TMutation>();

            // Dependency resolver
            services.AddSingleton<IDependencyResolver, GraphQLDependencyResolver>();

            // Registrer Queries
            foreach (var type in typeof(TQuery).GetTypeInfo().Assembly.GetTypes()
                .Where(type => !type.GetTypeInfo().IsAbstract && type.GetTypeInfo().IsSubclassOf(typeof(GraphQLBaseInformation))))
            {
                // Add query base
                services.AddSingleton(type);
                services.AddSingleton<GraphQLBaseInformation>(provider =>
                    (GraphQLBaseInformation) provider.GetService(type));
            }
            
            return services;
        }
    }
}
