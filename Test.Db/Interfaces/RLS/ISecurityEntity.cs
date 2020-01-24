﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Interfaces.RLS
{
    /// <summary>
    /// Required for security implementation
    /// </summary>
    public interface ISecurityEntity
    {
        long Id { get; set; }
    }
}
