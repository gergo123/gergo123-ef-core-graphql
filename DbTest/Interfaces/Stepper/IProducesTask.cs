using DbTest.Interfaces.Stepper;
using DbTest.Stepper.Model.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest.Interface
{
    public interface IProducesTask<K> : IValidateTasks<K>
        where K : TaskEntity
    {
        /// Should only be returning securityObjectIDs
        /// <summary>
        /// </summary>
        /// <returns></returns>
        long[] AssignTaskToSecurityObjects();
    }
}
