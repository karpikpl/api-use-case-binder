using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace common;

public static class SecurityExtensions
{
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services, params AuthPolicy[] policies)
    {
        services.AddAuthorizationCore(options =>
        {
            foreach (var policy in policies)
            {
                options.AddPolicy(policy.Name, p => p.Requirements.Add(policy));
            }
        });
        services.AddSingleton<IAuthorizationHandler, HeaderRequirementHandler>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "MockScheme";
            options.DefaultChallengeScheme = "MockScheme";
        }).AddScheme<AuthenticationSchemeOptions, MockAuthenticationHandler>("MockScheme", options => { });
        return services;
    }

    public class HeaderRequirementHandler : AuthorizationHandler<AuthPolicy>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HeaderRequirementHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthPolicy requirement)
        {
            var expectedRoles = string.Join(",", requirement.Roles);

            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("x-sample-auth", out var headerValues))
            {
                if (headerValues.Equals(expectedRoles))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }

    public class MockAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public MockAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[] { new Claim(ClaimTypes.Name, "TestUser") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}