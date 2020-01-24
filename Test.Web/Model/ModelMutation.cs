using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Web.Model
{
    public class ModelMutation : ObjectGraphType<object>
    {
        public ModelMutation()
        {
            Name = "Mutation";
        }
    }
}
