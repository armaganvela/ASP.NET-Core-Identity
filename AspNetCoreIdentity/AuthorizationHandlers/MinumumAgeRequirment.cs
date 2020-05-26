﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.AuthorizationHandlers
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public MinimumAgeRequirement(int age)
        {
            MinimumAge = age;
        }

        public int MinimumAge { get; set; }
    }
}
