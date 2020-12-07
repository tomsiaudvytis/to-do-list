using Common.Configurations;
using Common.Interfaces.Repositories;
using Common.Interfaces.Services;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Extensions;
using WebApi.Middlewares;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(o => o.UseMySql(Configuration.GetConnectionString("ToDoItems")));

            services.AddSwaggerConfiguration();
            services.AddJwtAuthentication(Configuration["Authentication:Secret"]);

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IToDoITemService, ToDoITemService>();
            services.AddScoped<ILogger, Logger.Logger>();


            services.Configure<Authentication>(options => Configuration.GetSection("Authentication").Bind(options));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(swagger => swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo Api"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}