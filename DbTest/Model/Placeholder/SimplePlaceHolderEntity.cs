using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest.Model.Placeholder
{
    public class SimplePlaceHolderEntity
    {
        public int Id { get; set; }
        public string SimpleProperty { get; set; }

        public bool IsActive { get; set; }
    }
}
