using DbTest.Model.Placeholder;
using EFTest.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Rules.rules.Episode
{
    [Rule(typeof(SimplePlaceHolderEntity))]
    public class PlaceholderRule : IRule
    {
        public List<ValidationResult> Errors { get; set; } = new List<ValidationResult>();
        public PlaceholderRule()
        {
            // TODO Dependency injection
        }

        public void ExecuteRule(EntityEntry entry)
        {
            var entity = entry.Entity as SimplePlaceHolderEntity;

            // Validation...
        }

        public bool ShouldValidate(EntityEntry entry)
        {
            if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
            {
                return true;
            }

            return false;
        }
    }
}
