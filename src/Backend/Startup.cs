using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Backend.GraphQL.Helper.Authorization;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public IConfiguration Config { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add GraphQL
            services.RegistrerSchema<GraphQLQuery, GraphQLMutation>();
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddTransient<DataLoaderDocumentListener>();
            services.AddScoped<UserContextBuilder>();

            services.AddGraphQLHttp<UserContextBuilder>();
            
            // Setup repository
            if (Config["Repository"] == "InMemory")
            {
                // Add repositories
                services.AddMockRepository();
            }
            else
            {
                // Add EF Core
                services.AddDbContextPool<ApplicationContext>(options => options.UseSqlite("Data Source=Backend.db"));

                // Add repositories
                services.AddEfRepository();
            }

            // Remove all automatic mapping for inbound claims
            // Otherwise "sub" becomes "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            // Add Authentification
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Config["AuthenticationServer"];
                    options.RequireHttpsMetadata = true;
                    options.InboundJwtClaimTypeMap = new Dictionary<string, string>();

                    options.ApiName = "Nordic4HCamp-API";
                });

            // Authorization
            services.AddAuthorization(options =>
            {
                // client_ is prefixed in each claim.. (can be disabled)
                options.AddPolicy("Volunteer", policy => policy.RequireClaim("client_access", "Volunteer"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Auth
            app.UseAuthentication();

            // Add GraphQL Http Schema
            app.UseGraphQLHttp<GraphQLSchema<GraphQLQuery, GraphQLMutation>>(new GraphQLHttpOptions { Path = "/v1/graphql", ExposeExceptions = env.IsDevelopment(), ValidationRules = { new RequiresAuthValidationRule() } });

            // Use graphql-playground at url /v1/playground
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions { GraphQLEndPoint = "/v1/graphql", Path = "/v1/playground" });

            // Use GraphiQL at url /v1/help
            app.UseGraphiQLServer(new GraphiQLOptions { GraphQLEndPoint = "/v1/graphql", GraphiQLPath = "/v1/help" });
        }
    }
}
