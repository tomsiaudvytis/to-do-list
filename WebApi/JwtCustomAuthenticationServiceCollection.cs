using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace WebApi
{
    public static class JwtCustomAuthenticationServiceCollection
    {
        public static void AddJwtAuthentication(this IServiceCollection serviceCollection, string secretKey)
        {
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                             .AddJwtBearer(options =>
                             {
                                 options.RequireHttpsMetadata = false;
                                 options.SaveToken = true;
                                 options.TokenValidationParameters = new TokenValidationParameters
                                 {
                                     ValidateIssuer = false,
                                     ValidateAudience = false,
                                     ValidateIssuerSigningKey = true,
                                     IssuerSigningKey =
                                         new SymmetricSecurityKey(
                                             Encoding.UTF8.GetBytes(secretKey)),
                                     ClockSkew = TimeSpan.Zero
                                 };
                             });
        
            serviceCollection.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });
        }
    }
}