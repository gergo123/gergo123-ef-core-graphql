using DbTest.Repositories.RLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest.RLS
{
    public class CurrentUserProvider
    {
        public string CurrentUserIdentifier { get; private set; }
        public Model.RLS.SecurityIdentity Identity { get; private set; }

        public void SetUser(Model.RLS.SecurityIdentity identity)
        {
            Identity = identity;
        }

        public void SetIdentifier(string identifier)
        {
            CurrentUserIdentifier = identifier;
        }
    }
}
