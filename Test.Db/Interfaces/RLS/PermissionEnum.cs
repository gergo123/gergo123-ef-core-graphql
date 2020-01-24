using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Interfaces.RLS
{
    /// <summary>
    /// HasPermission:
    ///     (user & permission) != 0
    /// SetPermission:
    ///     user |= permission
    /// ClearPermission:
    ///     user &= ~permission
    /// </summary>
    public enum PermissionEnum
    {
        Read = 1,
        Update = 2,
        Delete = 4,

        Full = 127
    }
}
