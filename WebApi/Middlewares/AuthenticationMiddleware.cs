using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Configurations;
using Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Authentication _authentication;

        public AuthenticationMiddleware(IOptions<Authentication> authentication, RequestDelegate next)
        {
            _authentication = authentication.Value;
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?
                               .Split(" ")
                               .Last();

            if (token != null)
            {
                AttachUserToContext(context, userService, token);
            }

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_authentication.Secret);

                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out var validatedToken);

                var jwtToken = (JwtSecurityToken) validatedToken;
                var userMail = jwtToken.Claims.First(x => x.Type == "email").Value;

                context.Items["User"] = userService.GetByEmail(userMail);
            }
            catch
            {
                // ignored
            }
        }
    }
}