using DbTest.Interfaces.RLS;
using DbTest.Stepper.Model.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest.Stepper.Model.Workflow
{
    public class BasicTask : TaskEntity, ISecurityEntity
    {
        public string Message { get; set; }
    }
}
