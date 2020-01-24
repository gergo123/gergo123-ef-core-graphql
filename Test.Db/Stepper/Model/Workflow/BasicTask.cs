using Test.Db.Interfaces.RLS;
using Test.Db.Stepper.Model.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Stepper.Model.Workflow
{
    public class BasicTask : TaskEntity, ISecurityEntity
    {
        public string Message { get; set; }
    }
}
