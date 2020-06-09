using DbTest.Stepper.Model.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest.Stepper.Model.RLS
{
    public abstract class SecurityObject
    {
        public long Id { get; set; }

        public virtual List<BasicTask> AssignedTasks { get; set; } = new List<BasicTask>();
    }
}
