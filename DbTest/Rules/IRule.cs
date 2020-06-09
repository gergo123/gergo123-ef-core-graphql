using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Rules
{
    public interface IRule
    {
        /// <summary>
        /// Collection for errors during ExecuteRule.
        /// </summary>
        List<ValidationResult> Errors { get; set; }

        /// <summary>
        /// Validation logic.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        void ExecuteRule(EntityEntry entry);

        /// <summary>
        /// Returns whether the rule should be executed depending on the entry (ex.: entry's state).
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        bool ShouldValidate(EntityEntry entry);
    }
}
