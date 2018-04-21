using System;
using Backend.GraphQL.Helper.Connections;
using Backend.GraphQL.Helper.Schema;
using Backend.GraphQL.Repository;
using Backend.GraphQL.Types;
using Backend.Model;

namespace Backend.GraphQL.Schema.Query
{
    [RegistrerQuery]
    public class AllPagesQuery : UnidirectionalConnectionQuery<PageGraphType, RepositoryUnidirectionalConnectionQueryExecutor<Page>>
    {
        public AllPagesQuery(IServiceProvider serviceProvider) : base(serviceProvider, "Pages")
        {
            Description = "Get all the pages";
        }
    }
}
