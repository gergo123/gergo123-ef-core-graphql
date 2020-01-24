using Test.Db.Interfaces.RLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Stepper.Model.Workflow
{
    public class BasicTaskAcl : IACLEntity
    {
        public long ID { get; set; }
        public long EntityID { get; set; }
        public PermissionEnum Permission { get; set; }
        public long SecurityObjectID { get; set; }
    }
}
