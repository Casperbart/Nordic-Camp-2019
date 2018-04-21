using System;
using System.Security.Claims;
using System.Threading.Tasks;
using GraphQL.Server.Transports.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.GraphQL.Helper.Authorization
{
    /// <summary>
    /// Contains the GraphQL user context used in executing of the fields to get information of the current user capablities
    /// </summary>
    public class GraphQLUserContext
    {
        /// <summary>
        /// Constructs a new GraphQLUserContext
        /// </summary>
        /// <param name="authorizationService">The IAuthorizationService used to evaluate authorization policies</param>
        /// <param name="isAuthenticated">True if the request is authentificated</param>
        /// <param name="user">The ClaimPrincipal for the request</param>
        public GraphQLUserContext(IAuthorizationService authorizationService, bool isAuthenticated,
            ClaimsPrincipal user)
        {
            AuthorizationService =
                authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            IsAuthenticated = isAuthenticated;
            User = user ?? throw new ArgumentNullException(nameof(user));
        }

        /// <summary>
        /// AuthorizationService used to evaluate authorization policies
        /// </summary>
        public IAuthorizationService AuthorizationService { get; set; }

        /// <summary>
        /// True if the request is authentificated
        /// </summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// The ClaimPrincipal for the request
        /// </summary>
        public ClaimsPrincipal User { get; set; }
    }

    /// <summary>
    /// UserContext builder which builds a <see cref="GraphQLUserContext"/> from a <see cref="HttpContext"/>
    /// </summary>
    public class UserContextBuilder : IUserContextBuilder
    {
        /// <inheritdoc />
        public Task<object> BuildUserContext(HttpContext httpContext)
        {
            return Task.FromResult((object)new GraphQLUserContext(httpContext.RequestServices.GetRequiredService<IAuthorizationService>(),
                httpContext.User?.Identity?.IsAuthenticated ?? false, httpContext.User));
        }
    }
}