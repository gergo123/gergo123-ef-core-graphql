using DbTest.Stepper.Model.RLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest.Model.RLS
{
    public class SecurityIdentity : SecurityObject
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        // Ex.: Username
        public string Identifier { get; set; }

        public virtual List<SecurityGroupSecurityIdentity> GroupMemberShips { get; set; } = new List<SecurityGroupSecurityIdentity>();
    }
}
