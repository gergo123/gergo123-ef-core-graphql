using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Stepper.Model
{
    public enum TaskStatus
    {
        @default = 0,
        Pending,
        Approved,
        Declined
    }
}
