﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentity.Controllers
{
    [Authorize(Policy = "RequireOverallRoles")]
    public class PolicyRoleAuthorizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}