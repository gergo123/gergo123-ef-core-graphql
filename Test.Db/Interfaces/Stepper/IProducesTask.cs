using Test.Db.Interfaces.Stepper;
using Test.Db.Stepper.Model.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Interface
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
