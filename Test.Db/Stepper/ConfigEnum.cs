using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Stepper
{
    public enum TaskAssignType
    {
        Default = 0,
        /// <summary>
        /// Egy feladat tobb securityobject-nek
        /// </summary>
        AssignOne = 1,

        /// <summary>
        /// Minden securityobject-nek külön feladat
        /// </summary>
        AssignMany
    }
}
