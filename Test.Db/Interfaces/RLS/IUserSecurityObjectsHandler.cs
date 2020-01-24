using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.RLS
{
    public interface IUserSecurityObjectsHandler
    {
        IEnumerable<long> SecurityObjects { get; }
    }
}
