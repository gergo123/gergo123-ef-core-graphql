using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest.Utils
{
    public class UserRegisterFromAd : IDisposable
    {
        private readonly PrincipalContext _principalContext;
        private readonly string _domain = "MVMH.LOCAL";
        public UserRegisterFromAd()
        {
            _principalContext = new PrincipalContext(ContextType.Domain, _domain);
        }

        public UserPrincipal GetADUser(string loginname)
        {
            return UserPrincipal.FindByIdentity(_principalContext, loginname);
        }

        public void Dispose()
        {
            _principalContext.Dispose();
        }
    }
}
