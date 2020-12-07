using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace WebApi.Attributes
{
    public class AuthorizedToDoAction : TypeFilterAttribute
    {
        public AuthorizedToDoAction() : base(typeof(AuthorizedToDoActionFilter))
        {
        }
    }

    public class AuthorizedToDoActionFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims.ToList();
            var isAdmin = claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault()?.Value == UserRoles.Admin;

            if (!isAdmin)
            {
                var userId = claims.Where(x => x.Type == "user_id").FirstOrDefault()?.Value;
           
                //validate if user accessing it's own object
            }
        }
    }
}
