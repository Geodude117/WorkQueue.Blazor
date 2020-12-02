using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CallBack_Model.Model;
using DebtManager3NotesModels.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkQueue.Blazor.Data;
using WorkQueue.Blazor.Helpers;

namespace WorkQueue.Blazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<HttpClient>();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddHttpContextAccessor();
            services.AddSingleton<QueueGroupService>();
            services.AddSingleton<QueueItemService>();
            services.AddSingleton<QuestionService>();
            services.AddSingleton<CSUCallbackService>();
            services.AddSingleton<CustomMapper>();


            services.AddTransient<IHttpConnectionFactory<CSU_Callback>, HttpConnectionFactory<CSU_Callback>>();
            services.AddTransient<IHttpConnectionFactory<QueueGroup>, HttpConnectionFactory<QueueGroup>>();
            services.AddTransient<IHttpConnectionFactory<QResult>, HttpConnectionFactory<QResult>>();
            services.AddTransient<IHttpConnectionFactory<QueueItem>, HttpConnectionFactory<QueueItem>>();
            services.AddTransient<IHttpConnectionFactory<QItemHolder>, HttpConnectionFactory<QItemHolder>>();
            services.AddTransient<IHttpConnectionFactory<NotesViewModel>, HttpConnectionFactory<NotesViewModel>>();

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
