using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace common;

public static class UseCaseProvider
{

    public static UseCaseName GetUseCase(IHttpContextAccessor contextAccessor)
    {
        return GetUseCase(contextAccessor.HttpContext);
    }

    public static UseCaseName GetUseCase(HttpContext httpContext)
    {
        var useCaseFromRoute = httpContext.GetRouteValue("useCase")?.ToString();

        if (useCaseFromRoute!= null && Enum.TryParse<UseCaseName>(useCaseFromRoute, true, out var useCase))
        {
            return useCase;
        }

        return UseCaseName.None;
    }
}