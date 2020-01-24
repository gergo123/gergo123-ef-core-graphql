using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Stepper
{
    public class StateTransitionException : Exception
    {
        public StateTransitionException(string message) : base(message)
        {
        }
    }
}
