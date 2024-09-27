using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace common;

public class UseCaseAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    public UseCaseAuthorizeAttribute(UseCaseName useCase)
    {
        UseCase = useCase;
    }

    public string PolicyName { get; set; }
    public UseCaseName UseCase { get; }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // Example condition: Check if a route value matches the condition
        var routeValue = context.RouteData.Values["useCase"]?.ToString();
        if (routeValue != null && Enum.TryParse<UseCaseName>(routeValue, ignoreCase: true, out var routeUseCase))
        {
            if (routeUseCase == UseCase)
            {
                // Apply the policy if the condition is met
                var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
                var policy = await context.HttpContext.RequestServices.GetRequiredService<IAuthorizationPolicyProvider>()
                    .GetPolicyAsync(PolicyName);

                var result = await authorizationService.AuthorizeAsync(context.HttpContext.User, null, policy);

                if (!result.Succeeded)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}