using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Model.RLS
{
    public class SecurityGroupSecurityIdentity
    {
        public long SecurityGroupId { get; set; }
        public SecurityGroup Group { get; set; }

        public long SecurityIdentityId { get; set; }
        public SecurityIdentity Identity { get; set; }
    }
}
