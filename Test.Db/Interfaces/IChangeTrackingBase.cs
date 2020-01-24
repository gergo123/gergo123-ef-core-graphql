using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Interfaces
{
    public interface IChangeTrackingBase
    {
        string UserCreated { get; set; }
        DateTime DateCreated { get; set; }

        string UserModified { get; set; }
        DateTime? DateModified { get; set; }
    }
}
