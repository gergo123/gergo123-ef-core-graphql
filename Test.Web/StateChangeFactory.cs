using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Web
{
    public class StateChangeFactory
    {
        private readonly IServiceProvider serviceProvider;

        public StateChangeFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public object CreateChange(Type changeType)
        {
            var dependencies = new List<object>();
            var constr = changeType.GetConstructors().First();
            foreach (var param in constr.GetParameters())
            {
                dependencies.Add(serviceProvider.GetService(param.ParameterType));
            }

            //changeType = changeType.MakeGenericType(new Type[] { typeof(T), typeof(C) });
            return Activator.CreateInstance(changeType, dependencies.ToArray());
        }
    }
}
