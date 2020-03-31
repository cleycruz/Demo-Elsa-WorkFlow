using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elsa.Activities.Http.Extensions;
using Elsa.Activities.Timers.Extensions;
using Elsa.Persistence.EntityFrameworkCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
using Elsa.Persistence.EntityFrameworkCore;
using Elsa.Persistence.EntityFrameworkCore.DbContexts;
using Elsa.Dashboard.Extensions;
using Elsa.Activities.Workflows.Extensions;
using Elsa.Activities.ControlFlow.Extensions;
using Elsa.Activities.Email.Extensions;
using Elsa.Activities.Console.Extensions;
using Elsa.Activities.UserTask.Extensions;

namespace Demo
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
            services.AddRazorPages();
            services
            
            .AddElsa(elsa => elsa.AddEntityFrameworkStores<SqlServerContext>(ef => ef.UseSqlServer(Configuration.GetConnectionString("SqlServer"))));
        
            services
            .AddElsaDashboard();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
           
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            // Register necessary ASP.NET Core middleware that triggers workflows containing HTTP activities.
            app.UseHttpActivities();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
