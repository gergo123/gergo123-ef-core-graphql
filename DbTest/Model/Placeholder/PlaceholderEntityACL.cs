using DbTest.Interfaces.RLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest.Model.Placeholder
{
    public class PlaceholderEntityACL : IACLEntity
    {
        public long ID { get; set; }

        public long EntityID { get; set; }
        public PermissionEnum Permission { get; set; }
        public long SecurityObjectID { get; set; }
    }
}
