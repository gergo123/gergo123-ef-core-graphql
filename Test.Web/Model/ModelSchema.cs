using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Web.Model
{
    public class ModelSchema : Schema
    {
        public ModelSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<ModelQuery>();
            //Mutation = resolver.Resolve<ModelMutation>();
        }
    }
}
