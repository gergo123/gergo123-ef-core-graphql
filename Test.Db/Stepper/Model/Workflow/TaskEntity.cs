using Test.Db.Interfaces;
using Test.Db.Model.RLS;
using Test.Db.Stepper;
using Test.Db.Stepper.Model;
using Test.Db.Stepper.Model.Workflow;
using System;

namespace Test.Db.Stepper.Model.Workflow
{
    public abstract class TaskEntity : ITask, IChangeTrackingBase
    {
        public long Id { get; set; }

        /// <summary>
        /// Id pointing at the "usefull" record, holding all the data neccessary for our flow
        /// </summary>
        public long EntityId { get; set; }
        public TaskStatus Status { get; set; }
        public string UniqueID { get; set; }

        public DateTime ApprovedAt { get; set; }

        public SecurityIdentity ApprovedBySecurityIdentity { get; set; } = new SecurityIdentity();
        public long? ApprovedBySecurityIdentityID { get; set; }

        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserModified { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual void ChangeStatus(SecurityIdentity approvedBy, TaskStatus newTaskStatus)
        {
            // need this because of group approval logic
            ApprovedAt = DateTime.Now;
            Status = newTaskStatus;
            ApprovedBySecurityIdentityID = approvedBy.Id;
        }
    }
}
