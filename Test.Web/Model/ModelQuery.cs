using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Db.Model.Placeholder;
using Test.Web.GraphQl;

namespace Test.Web.Model
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
