using System.Collections.Generic;
using GraphQL;
using GraphQL.Builders;
using GraphQL.Types;

namespace Backend.GraphQL.Helper.Authorization
{
    /// <summary>
    /// Contains extention methods to require authentification or authorization of specific GraphQL fields
    /// </summary>
    public static class GraphQLAuthorizationExtensions
    {
        /// <summary>
        /// Requires the request to be authentificated for executing the specific GraphQL field
        /// </summary>
        /// <param name="type">The GraphQL field which the authentification should be applied to</param>
        public static void RequireAuthenfication(this IProvideMetadata type)
        {
            type.Metadata[RequiresAuthValidationRule.RequireAuthentifcationKey] = true;
        }

        /// <summary>
        /// Requires the request to be authentificated for executing the specific GraphQL field
        /// </summary>
        /// <typeparam name="TSourceType">The type which is mapped from in the builder</typeparam>
        /// <typeparam name="TReturnType">The GraphQL type which is mapped to</typeparam>
        /// <param name="builder">The GraphQL field which the authentification should be applied to</param>
        /// <returns>The same builder recieved in the parameter <paramref name="builder"/></returns>
        public static FieldBuilder<TSourceType, TReturnType> RequireAuthenfication<TSourceType, TReturnType>(
            this FieldBuilder<TSourceType, TReturnType> builder)
        {
            builder.FieldType.RequireAuthenfication();
            return builder;
        }

        /// <summary>
        /// Requires a specific authorization pulicy defined using default authorization in ASP.NET Core
        /// </summary>
        /// <param name="type">The GraphQL field which the authentification should be applied to</param>
        /// <param name="policy">The authorization policy which should be successfully evaluated for executing the field</param>
        public static void RequireAuthorizePolicy(this IProvideMetadata type, string policy)
        {
            var policies = type.GetMetadata<List<string>>(RequiresAuthValidationRule.PoliciesKey);

            if (policies == null)
            {
                policies = new List<string>();
                type.Metadata[RequiresAuthValidationRule.PoliciesKey] = policies;
            }

            policies.Fill(policy);
        }

        /// <summary>
        /// Requires a specific authorization pulicy defined using default authorization in ASP.NET Core
        /// </summary>
        /// <typeparam name="TSourceType">The type which is mapped from in the builder</typeparam>
        /// <typeparam name="TReturnType">The GraphQL type which is mapped to</typeparam>
        /// <param name="builder">The GraphQL field which the authorization policy should be applied to</param>
        /// <param name="policy">The authorization policy which should be successfully evaluated for executing the field</param>
        /// <returns>The same builder recieved in the parameter <paramref name="builder"/></returns>
        public static FieldBuilder<TSourceType, TReturnType> RequireAuthorizePolicy<TSourceType, TReturnType>(
            this FieldBuilder<TSourceType, TReturnType> builder, string policy)
        {
            builder.FieldType.RequireAuthorizePolicy(policy);
            return builder;
        }
    }
}