using Microsoft.AspNetCore.Authorization;
using Service2.Configuration;

namespace Service2;

public class Service2AuthHandler : AuthorizationHandler<IAuthorizationRequirement, HttpContext>
{
    private readonly AuthorizationSettings _authorizationSettings;

    public Service2AuthHandler(IConfiguration configuration)
    {
        _authorizationSettings = configuration.GetSection("Authorization").Get<AuthorizationSettings>();
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        IAuthorizationRequirement requirement,
        HttpContext resource)
    {
        if (context.User.HasClaim("groups", _authorizationSettings.GroupId))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        context.Fail();
        return Task.CompletedTask;
    }
}