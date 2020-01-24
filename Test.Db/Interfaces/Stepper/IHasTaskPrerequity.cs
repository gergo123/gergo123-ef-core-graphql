using Test.Db.Interfaces.Stepper;
using Test.Db.Stepper.Model.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Interface.Stepper
{
    public interface IHasTaskPrerequity
    {
        void SetPreviousStep(string nameOfPreviousObject);
        ITask[] getCreatedTask();
    }
}
