using Microsoft.AspNetCore.Authorization;

namespace common;

public class AuthPolicy: IAuthorizationRequirement
{
    private AuthPolicy(string name, params string[] roles)
    {
        Name = name;
        Roles = roles;
    }

    public string Name { get; set; }
    public string[] Roles { get; set; }

    public static AuthPolicy WestCoastPolicy => new AuthPolicy(AuthPolicyName.WestCoastPolicy, "west-user");
    public static AuthPolicy EastCoastPolicy => new AuthPolicy(AuthPolicyName.EastCoastPolicy, "east-user");
}