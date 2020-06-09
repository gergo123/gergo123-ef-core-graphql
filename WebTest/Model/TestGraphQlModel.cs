using DbTest.Model.Placeholder;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Model
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
