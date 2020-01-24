using Test.Db.Interfaces.RLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Model.Placeholder
{
    public partial class PlaceholderEntity : ISecurityEntity
    {
        public long Id { get; set; }

        // Additional props goes here...
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
