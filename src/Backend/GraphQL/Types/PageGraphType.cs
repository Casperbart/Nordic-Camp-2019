using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;
using GraphQL.Types;

namespace Backend.GraphQL.Types
{
    public class PageGraphType : ObjectGraphType<Page>
    {
        public PageGraphType()
        {
            Field(e => e.Url).Description("Contains the url for the current subpage");
            Field(e => e.Content).Description("Contains the content of the page");
        }
    }
}
