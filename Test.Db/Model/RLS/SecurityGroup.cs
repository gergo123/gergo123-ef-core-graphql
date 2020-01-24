using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Model.RLS
{
    public class SecurityGroup : SecurityObject
    {
        public string Name { get; set; }

        public virtual List<SecurityGroupSecurityIdentity> GroupMembers { get; set; } = new List<SecurityGroupSecurityIdentity>();
    }
}
