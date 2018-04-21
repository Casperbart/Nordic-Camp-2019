using System.Net.Http;
using System.Threading.Tasks;
using Backend.GraphQL.Helper.Schema;
using GraphQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SAHB.GraphQLClient.Executor;
using SAHB.GraphQLClient.Result;

namespace Backend.Tests.GraphQLMock
{
    public class GraphQLExecutor : IGraphQLHttpExecutor
    {
        private readonly GraphQLSchema<GraphQLQuery, GraphQLMutation> _schema;
        private readonly IDocumentExecuter _executor;

        public GraphQLExecutor(GraphQLSchema<GraphQLQuery, GraphQLMutation> schema, IDocumentExecuter executor)
        {
            _schema = schema;
            _executor = executor;
        }

        public async Task<GraphQLDataResult<T>> ExecuteQuery<T>(string query, string url, HttpMethod method, string authorizationToken = null,
            string authorizationMethod = "Bearer") where T : class
        {
            var request = JsonConvert.DeserializeObject<GraphQLRequestQuery>(query);

            var result = await _executor.ExecuteAsync(_ =>
            {
                _.Query = request.Query;
                _.OperationName = request.OperationName;
                _.Inputs = request.Variables.ToInputs();
                _.Schema = _schema;
            });

            var jsonResult = JsonConvert.SerializeObject(result);

            return JsonConvert.DeserializeObject<GraphQLDataResult<T>>(jsonResult);
        }

        public class GraphQLRequestQuery
        {
            [JsonProperty("query")]
            public string Query { get; set; }

            [JsonProperty("variables")]
            public JObject Variables { get; set; }

            [JsonProperty("operationName")]
            public string OperationName { get; set; }

            public Inputs GetInputs()
            {
                return GetInputs(Variables);
            }

            public static Inputs GetInputs(JObject variables)
            {
                return variables.ToInputs();
            }
        }
    }
}