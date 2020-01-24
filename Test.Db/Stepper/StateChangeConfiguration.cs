using Test.Db.Model.RLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Stepper
{
    public class StateChangeConfiguration
    {
        // Allowed securityId list GetPermissions
        // Allowed status transitions GetAllowedStateChanges

        /// <summary>
        /// List of users which are able to perform this state change.
        /// Should store securityObjectIds somewhere in a static location. ex.: AdministratorsId = 1
        /// </summary>
        public SecurityObject[] HasPermissionToFullfillChange { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        public Enum[] AllowedEndStates { get; set; }
        /// <summary>
        /// Optional
        /// </summary>
        public Enum[] AllowedStartStates { get; set; }

        /// <summary>
        /// Egy feladatot kapjon meg több user, vagy minden user kapjon egy feladatot.
        /// </summary>
        public TaskAssignType TaskAssignType { get; set; }
    }
}
