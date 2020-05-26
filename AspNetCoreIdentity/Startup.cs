using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreIdentity.Data;
using AspNetCoreIdentity.Models;
using AspNetCoreIdentity.Services;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreIdentity.AuthorizationHandlers;

namespace AspNetCoreIdentity
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
            
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Over25AgeOnly", policy => policy.Requirements.Add(new MinimumAgeRequirement(25)));
            //});

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("OnlyAdmin", policy => policy.RequireClaim("isAdmin", "true"));
            //});

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireOverallRoles", policy => policy.RequireRole("Admin", "Client"));
            });

            // existing code above
            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            Initialize.InitializeData(app.ApplicationServices);
        }
    }
}
