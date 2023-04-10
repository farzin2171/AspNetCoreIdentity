using Microsoft.AspNetCore.Authorization;

namespace WebApp.Authorization
{
    public class HRManagerRequirment : IAuthorizationRequirement
    {
        public HRManagerRequirment(int probabtionMonth)
        {
            ProbabtionMonth = probabtionMonth;
        }

        public int ProbabtionMonth { get; }
    }

    public class HRManagerRequirmentHandler : AuthorizationHandler<HRManagerRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerRequirment requirement)
        {
            if (!context.User.HasClaim(x => x.Type == "EmploymentDate"))
            {
                return Task.CompletedTask;
            }
            var empDate = DateTime.Parse(context.User.FindFirst(x => x.Type == "EmploymentDate").Value);
            var period = DateTime.Now - empDate;

            if(period.Days >  requirement.ProbabtionMonth)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
