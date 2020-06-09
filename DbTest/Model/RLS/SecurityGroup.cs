using DbTest.Stepper.Model.RLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest.Model.RLS
{
    public class SecurityGroup : SecurityObject
    {
        public string Name { get; set; }

        public virtual List<SecurityGroupSecurityIdentity> GroupMembers { get; set; } = new List<SecurityGroupSecurityIdentity>();
    }
}
