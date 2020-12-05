using System;
using System.Text;
using Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace WebApi.Extensions
{
    public static class ServiceCollectionExtensions
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
                config.AddPolicy(UserRoles.Admin, Policies.AdminPolicy());
                config.AddPolicy(UserRoles.User, Policies.UserPolicy());
            });
        }

        public static void AddSwaggerConfiguration(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "HomeWork",
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Jwt Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Authorization using Bearer."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }
    }
}