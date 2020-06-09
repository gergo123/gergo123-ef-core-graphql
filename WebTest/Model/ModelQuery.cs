using DbTest.Model.Placeholder;
using WebTest.GraphQl;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Model
{
    public class ModelQuery : ObjectGraphType
    {
        public ModelQuery(ContextServiceLocator locator)
        {
            Name = "Query";

            Field<TestGraphQlModel>("PlaceholderEntity", resolve: context => new PlaceholderEntity
            {
                Id = 1
            });
        }
    }
}
