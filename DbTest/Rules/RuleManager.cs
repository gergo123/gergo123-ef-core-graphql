using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Rules
{
    public class RuleManager
    {
        private Dictionary<Type, List<Type>> classList;
        private bool isEnabled { get; set; } = true;

        public RuleManager()
        {
            classList = new Dictionary<Type, List<Type>>();

            collectClasses();
        }

        /// <summary>
        /// Disables the validation engine!
        /// </summary>
        /// <param name="isEnabled"></param>
        public void SetIsEnable(bool isEnabled) =>
            this.isEnabled = isEnabled;

        /// <summary>
        /// Collects every business rule, and makes a list of them.
        /// One rule can be executed for multiple entites.
        /// </summary>
        private void collectClasses()
        {
            foreach (Assembly item in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!item.GlobalAssemblyCache)
                {
                    foreach (Type type in item.GetTypes())
                    {
                        foreach (RuleAttribute cmd in type.GetCustomAttributes<RuleAttribute>())
                        {
                            if (type.GetInterfaces().Contains(typeof(IRule)))
                            {
                                var keys = cmd.EntityTriggeringTypes;
                                foreach (var key in keys)
                                {
                                    if (!classList.ContainsKey(key))
                                    {
                                        classList.Add(key, new List<Type>() { type });
                                    }
                                    else
                                    {
                                        classList[key].Add(type);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Executes the rule for the entry, if the <see cref="isEnabled"/> property is true.
        /// </summary>
        /// <param name="entry">Entry to run the rule on.</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> ExecuteRules(EntityEntry entry)
        {
            if (!this.isEnabled) { return new List<ValidationResult>(); }

            var baseRuleType = entry.Entity.GetType();
            Debug.WriteLine($"---ExecuteRules for : {baseRuleType.FullName} ---");
            List<IRule> ruleInstances = new List<IRule>();

            var implementedInterFaces = getImplementedTypes(baseRuleType);

            //TODO: run rule immediately?
            foreach (var ruleType in implementedInterFaces)
            {
                //Try to delegate the received command
                if (classList.Keys.Contains(ruleType))
                {
                    foreach (var ruleTypeItem in classList[ruleType])
                    {
                        var ruleInstance = Activator.CreateInstance(ruleTypeItem) as IRule;
                        ruleInstances.Add(ruleInstance);
                        Debug.WriteLine($"Adding rule for {ruleType.FullName} - {ruleInstance.GetType().FullName}");
                    }
                }
                else
                {
                    Debug.WriteLine($"No rule defined for entity type: {ruleType}");
                }
            }

            List<ValidationResult> validationResults = new List<ValidationResult>();
            //Run the rules
            foreach (var ruleInstance in ruleInstances)
            {
                Debug.WriteLine($"Running rule {ruleInstance.GetType().FullName}");
                var shouldValidate = ruleInstance.ShouldValidate(entry);
                Debug.WriteLine($"Running ShouldValidate, result: {shouldValidate}");
                if (shouldValidate)
                {
                    ruleInstance.ExecuteRule(entry);
                    validationResults.AddRange(ruleInstance.Errors);
                    Debug.WriteLine($"Added {ruleInstance.Errors.Count} errors");
                }
            }

            return validationResults;
        }

        /// <summary>
        /// Returns the given obj type, and the obj base types.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private IEnumerable<Type> getImplementedTypes(Type obj)
        {
            List<Type> tList = new List<Type>();
            Type t = obj;
            do
            {
                tList.Add(t);
                t = getBaseType(t);
            } while (t != typeof(object));

            return tList;
        }

        private Type getBaseType(Type obj)
        {
            return obj.BaseType;
        }
    }
}
