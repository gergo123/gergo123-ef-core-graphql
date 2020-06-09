using DbTest.Interfaces;
using DbTest.Model.RLS;
using DbTest.Stepper;
using DbTest.Stepper.Model;
using DbTest.Stepper.Model.RLS;
using DbTest.Stepper.Model.Workflow;
using System;

namespace DbTest.Stepper.Model.Workflow
{
    public abstract class TaskEntity : ITask, IChangeTrackingBase
    {
        public long Id { get; set; }

        /// <summary>
        /// A feladathoz tartozó entitás azonosítója ami a "hasznos" adatokat tartalmazza
        /// </summary>
        public long EntityId { get; set; }
        public TaskStatus Status { get; set; }
        public string UniqueID { get; set; }

        public DateTime ApprovedAt { get; set; }

        public SecurityIdentity ApprovedBySecurityIdentity { get; set; }
        public long? ApprovedBySecurityIdentityID { get; set; }

        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserModified { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedSecurityId { get; set; }
        public long? UpdatedSecurityId { get; set; }

        public virtual void ChangeStatus(SecurityIdentity approvedBy, TaskStatus newTaskStatus)
        {
            // need this because of group approval logic
            ApprovedAt = DateTime.Now;
            Status = newTaskStatus;
            ApprovedBySecurityIdentityID = approvedBy.Id;
        }
    }
}
