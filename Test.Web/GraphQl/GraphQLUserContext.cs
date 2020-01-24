using System.Collections.Generic;
using System.Security.Claims;

namespace Test.Web.GraphQl
{
    public class GraphQLUserContext : Dictionary<string, object>
    {
        public ClaimsPrincipal User { get; set; }
    }
}
