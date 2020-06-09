using DbTest.Model.RLS;
using DbTest.Stepper.Model.RLS;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbTest.Stepper.Model.Workflow
{
    public interface ITask
    {
        long Id { get; set; }
        /// <summary>
        /// A feladathoz tartozó entitás azonosítója ami a "hasznos" adatokat tartalmazza
        /// </summary>
        long EntityId { get; set; }
        TaskStatus Status { get; set; }
        string UniqueID { get; set; }

        DateTime ApprovedAt { get; set; }

        /// <summary>
        /// Required because a possibility of a group having to finish this task,
        ///  and than storing which user finished it from the assigned group.
        /// </summary>
        SecurityIdentity ApprovedBySecurityIdentity { get; set; }
        long? ApprovedBySecurityIdentityID { get; set; }

        void ChangeStatus(SecurityIdentity approvedBy, TaskStatus newTaskStatus);
    }
}
