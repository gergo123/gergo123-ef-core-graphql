using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbTest.Model.RLS;

namespace DbTest
{
    public static class DefaultData
    {
        public static SecurityGroup AdminGroup = new SecurityGroup
        {
            // Assuming this groups gets the first ID in the table
            Id = 1,
            Name = "Administrators"
        };
    }
}
