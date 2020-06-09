using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Rules
{
    public static class RuleFactory
    {
        private static RuleManager _ruleManager;
        public static RuleManager GetRuleManager()
        {
            _ruleManager = _ruleManager ?? new RuleManager();
            return _ruleManager;
        }
    }
}
