﻿using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Model
{
    public class ModelMutation : ObjectGraphType<object>
    {
        public ModelMutation()
        {
            Name = "Mutation";
        }
    }
}
