using Test.Db.Stepper.Model.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Interfaces.Stepper
{
    public interface IValidateTasks<K>
        where K : TaskEntity
    {
        void AlterTasks(K task);
    }
}
