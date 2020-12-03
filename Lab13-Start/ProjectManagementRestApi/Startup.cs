using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectManagementWebApp.ConfigurationExtensions;
using ProjectManagementWebApp.Data;
using ProjectManagementWebApp.Services;

namespace ProjectManagementRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
         

            services.AddDbContext<ProjectManagementContext>(options =>
                    //options.UseSqlServer(Configuration.GetConnectionString("ProjectManagementContext"))
                    options.UseInMemoryDatabase("ProjectManagementWebApp")
                    );
            services.ConfigureIdentity();
            services.AddControllers();

            services.AddScoped<ProjectAppService>();
            services.AddScoped<UserAppService>();
            services.AddScoped<CurrentUserService>();
            services.AddScoped<TaskAppService>();
            services.AddScoped<FileAppService>();
            services.AddScoped<CommentAppService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
