using Test.Db.Stepper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Stepper.Model.Workflow
{
    public class TestEntityModel : StateChangeEntityBase<TestEntityStates>
    {
        public string Message { get; set; }
    }
}
