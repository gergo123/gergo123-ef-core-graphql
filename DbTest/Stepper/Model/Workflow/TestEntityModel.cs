using DbTest.Stepper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest.Stepper.Model.Workflow
{
    public class TestEntityModel : StateChangeEntityBase<TestEntityStates>
    {
        public string Message { get; set; }
    }
}
