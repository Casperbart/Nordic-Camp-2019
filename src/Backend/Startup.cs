using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.GraphQL.Helper.Builder;
using Backend.GraphQL.Helper.Schema;
using Backend.Repository.EF;
using Backend.Repository.Mock;
using GraphQL.DataLoader;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Backend
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add GraphQL
            services.AddGraphQLHttp();
            services.RegistrerSchema<GraphQLQuery, GraphQLMutation>();
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddTransient<DataLoaderDocumentListener>();

            // Add EF Core
            services.AddDbContextPool<ApplicationContext>(options => options.UseSqlite("Data Source=Backend.db"));

            // Add repositories
            services.AddEfRepository();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add GraphQL Http Schema
            app.UseGraphQLHttp<GraphQLSchema<GraphQLQuery, GraphQLMutation>>(new GraphQLHttpOptions { Path = "/v1/graphql", ExposeExceptions = env.IsDevelopment() });

            // Use graphql-playground at url /v1/playground
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions { GraphQLEndPoint = "/v1/graphql", Path = "/v1/playground" });

            // Use GraphiQL at url /v1/help
            app.UseGraphiQLServer(new GraphiQLOptions { GraphQLEndPoint = "/v1/graphql", GraphiQLPath = "/v1/help" });
        }
    }
}
