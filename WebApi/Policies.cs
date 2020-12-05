using Common;
using Microsoft.AspNetCore.Authorization;

namespace WebApi
{
    public class Policies
    {

        public static AuthorizationPolicy AdminPolicy()
            => new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                               .RequireRole(UserRoles.Admin)
                                               .Build();

        public static AuthorizationPolicy UserPolicy()
            => new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                               .RequireRole(UserRoles.User)
                                               .Build();
    }
}