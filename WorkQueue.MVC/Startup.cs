using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallBack_Model.Model;
using DebtManager3AccountModels.Models;
using DebtManager3NotesModels.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkQueue.MVC.Helpers;
using WorkQueue.MVC.Models;

namespace WorkQueue.MVC
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
            services.AddMvc();
            services.AddTransient<IHttpConnectionFactory<CSU_Callback>, HttpConnectionFactory<CSU_Callback>>();
            services.AddTransient<IHttpConnectionFactory<QueueGroup>, HttpConnectionFactory<QueueGroup>>();
            services.AddTransient<IHttpConnectionFactory<QResult>, HttpConnectionFactory<QResult>>();
            services.AddTransient<IHttpConnectionFactory<QueueItem>, HttpConnectionFactory<QueueItem>>();
            services.AddTransient<IHttpConnectionFactory<QItemHolder>, HttpConnectionFactory<QItemHolder>>();
            services.AddTransient<IHttpConnectionFactory<NotesViewModel>, HttpConnectionFactory<NotesViewModel>>();
            services.AddTransient<IHttpConnectionFactory<AccountModel>, HttpConnectionFactory<AccountModel>>();
            services.AddSingleton<IConfiguration>(x => Configuration);
            services.AddSingleton<IUserPermissionLogic, UserPermissionLogic>();
            services.AddSingleton(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
