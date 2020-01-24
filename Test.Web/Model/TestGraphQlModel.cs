using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Db.Model.Placeholder;

namespace Test.Web.Model
{
    public class TestGraphQlModel : ObjectGraphType<PlaceholderEntity>
    {
        public TestGraphQlModel()
        {
            Field(x => x.Id, true);
            //Field(typeof(string), "text",
            //    resolve: (context) =>
            //    {
            //        return "Hello world";
            //    });
        }
    }
}
