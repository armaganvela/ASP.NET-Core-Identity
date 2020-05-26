using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.AuthorizationHandlers
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "BirthDate"))
            {
                return Task.CompletedTask;
            }

            var birthDate = Convert.ToDateTime(context.User.FindFirst(c => c.Type == "BirthDate").Value);

            var userAge = DateTime.Today.Year - birthDate.Year;

            if (birthDate > DateTime.Today.AddYears(-userAge))
            {
                userAge--;
            }

            if (userAge >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

}
