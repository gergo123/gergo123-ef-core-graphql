using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Rules
{
    public class RuleAttribute : Attribute
    {
        public Type[] EntityTriggeringTypes
        {
            get; private set;
        }

        public RuleAttribute(params Type[] entity)
        {
            EntityTriggeringTypes = entity;
        }
    }
}
