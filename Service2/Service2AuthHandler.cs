using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Service2;

public class Service2AuthHandler : AuthorizationHandler<IAuthorizationRequirement, HttpContext>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorizationRequirement requirement,
        HttpContext resource)
    {
        if (context.User.HasClaim("groups", "f7245d37-90a4-42cb-97f4-e6ec2c5fe98f"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        context.Fail();
        return Task.CompletedTask;
    }
}