using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Language.AST;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Authorization;

namespace Backend.GraphQL.Helper.Authorization
{
    /// <summary>
    /// GraphQL validation rule used for evaluating Authentification and Authorization requirements
    /// </summary>
    public class RequiresAuthValidationRule : IValidationRule
    {
        public static readonly string PoliciesKey = "AuthorizationPolicies";
        public static readonly string RequireAuthentifcationKey = "RequireAuthentification";

        /// <summary>
        /// Returns validator which can validate authenfication and authorization requirements
        /// </summary>
        /// <param name="context">The GraphQL validationContext</param>
        /// <returns></returns>
        public INodeVisitor Validate(ValidationContext context)
        {
            var userContext = context.UserContext.As<GraphQLUserContext>();
            var authenticated = userContext.IsAuthenticated;

            return new EnterLeaveListener(_ =>
            {
                // All mutations requires that the request is authentificated
                _.Match<Operation>(op =>
                {
                    if (op.OperationType == OperationType.Mutation && !authenticated)
                    {
                        context.ReportError(new ValidationError(
                            context.OriginalQuery,
                            "auth-required",
                            $"Authorization is required to access {op.Name}.",
                            op));
                    }
                });

                // Check if authentification is required for a field
                _.Match<Field>(fieldAst =>
                {
                    var fieldDef = context.TypeInfo.GetFieldDef();
                    if (RequireAuthentfication(fieldDef) && !authenticated)
                    {
                        context.ReportError(new ValidationError(
                            context.OriginalQuery,
                            "auth-required",
                            $"Authorization is required to execute {fieldDef.Name}.",
                            fieldAst));
                    }
                });
                
                // Check if a policy is required for a field
                _.Match<Field>(fieldAst =>
                {
                    var fieldDef = context.TypeInfo.GetFieldDef();
                    if (RequiresPolicy(fieldDef) &&
                        (!authenticated || !(CanAccess(fieldDef, userContext).GetAwaiter().GetResult())))
                    {
                        context.ReportError(new ValidationError(
                            context.OriginalQuery,
                            "auth-required",
                            $"You are not authorized to run this query.",
                            fieldAst));
                    }
                });
            });
        }

        /// <summary>
        /// Returns true if a specific field requires authentification
        /// </summary>
        /// <param name="type">The GraphQL field</param>
        /// <returns>True if a specific field requires authentification</returns>
        private static bool RequireAuthentfication(IProvideMetadata type)
        {
            return type.GetMetadata<bool>(RequireAuthentifcationKey, false);
        }

        /// <summary>
        /// Returns true if a specific field has authorization policies requirements
        /// </summary>
        /// <param name="type">The GraphQL field</param>
        /// <returns>True if the field requires evaluation of authorization policies</returns>
        private static bool RequiresPolicy(IProvideMetadata type)
        {
            var policies = type.GetMetadata<IEnumerable<string>>(PoliciesKey, new List<string>());
            return policies.Any();
        }

        /// <summary>
        /// Returns true if all the required authorization policies evaluates to true
        /// </summary>
        /// <param name="type">The GraphQL field</param>
        /// <param name="userContext">The GraphQL user context to evaluate against</param>
        /// <returns>Returns true if all of the required authorization policies evaluated successfully</returns>
        private static async Task<bool> CanAccess(IProvideMetadata type, GraphQLUserContext userContext)
        {
            var authorizationService = userContext.AuthorizationService;
            var user = userContext.User;

            // Get all the required policies
            var policies = type.GetMetadata<IEnumerable<string>>(PoliciesKey, new List<string>());

            // Loop throught all policies and see if they are all successfull
            foreach (var permission in policies)
            {
                var result = await authorizationService.AuthorizeAsync(user, permission);
                if (!result.Succeeded)
                    return false;
            }

            return true;
        }
    }
}