using DbTest.Interfaces.Stepper;
using DbTest.Stepper.Model.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest.Interface.Stepper
{
    public interface IHasTaskPrerequity
    {
        void SetPreviousStep(string nameOfPreviousObject);
        ITask[] getCreatedTask();
    }
}
