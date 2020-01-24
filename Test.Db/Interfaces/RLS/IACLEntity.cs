using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Interfaces.RLS
{
    public interface IACLEntity
    {
        long ID { get; set; }
        long EntityID { get; set; }
        PermissionEnum Permission { get; set; }
        long SecurityObjectID { get; set; }
    }
}
