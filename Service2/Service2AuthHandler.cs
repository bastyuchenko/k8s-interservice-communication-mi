using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;
using Service2;
using System.Runtime;

internal class Service2AuthHandler : AuthorizationHandler<AuthGroupReq, HttpContext>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthGroupReq requirement, HttpContext resource)
    {
        if (context.User.HasClaim("groups", "kkkkkkkkkkkk-iii-iii-oooooooo") && IsInterServiceAllowed())
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        bool IsInterServiceAllowed() =>
            resource?.GetEndpoint()
                ?.Metadata.GetMetadata<ControllerActionDescriptor>()
                ?.EndpointMetadata.OfType<AllowInterserviceCommunicationAttribute>()
                .SingleOrDefault() != null;

        context.Fail();
        return Task.CompletedTask;
    }
}