using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Rules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RuleValidationAttribute : ValidationAttribute
    {
        public RuleValidationAttribute() : base()
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ruleManager = RuleFactory.GetRuleManager();
            var entityType = value.GetType();
            //TODO ez nem műkődik, nincs használatban
            //ValidationResult.Success
            //var convertedEntityType = TypeDescriptor.GetConverter(entityType).ConvertTo(value, entityType);
            typeof(RuleManager).GetMethod("ExecuteRulesG").MakeGenericMethod(entityType).Invoke(ruleManager, new[] { value });
            //RuleFactory.GetRuleManager().ExecuteRulesG<entityType>(value);

            return base.IsValid(value, validationContext);
        }
    }
}
