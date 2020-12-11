using Gdpr_Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using WebAPI_QBusiness;
using WebAPI_QBusiness.DPA_Breach_Logic;
using WebAPI_QBusiness.GDPR_Logic;
using WebAPI_QBusiness.Indivdual_Queues;
using WebAPI_QBusiness.QueueActions_Logic;
using WebAPI_QBusiness.QueueGroup_Logic;
using WebAPI_QBusiness.QueueResult;
using WebAPI_QRepository;

namespace WebAPI_Queue
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            services.AddApiVersioning();
            
            services.AddTransient<IUnitOfWork>(connection => new UnitOfWork(Configuration.GetConnectionString("DbConnection")));
            services.AddTransient<IQueueBusiness, QueueBusiness>();
            services.AddTransient<ICSUBusiness,CSUBusiness>();
            services.AddTransient<IBreachStage1Business, BreachStage1Business>();
            services.AddTransient<IBreachStage2Business, BreachStage2Business>();
            services.AddTransient<IQResultBusiness, QResultBusiness>();
            services.AddTransient<IQActionBusiness, QActionBusiness>();
            services.AddTransient<IQueueGroupBusiness, QueueGroupBusiness>();
            services.AddTransient<IGdprInterface, GdprBusiness>();
            services.AddTransient<IQBusiness, QBusiness>();
            services.AddSingleton<IConfiguration>(x => Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "Callback Web API V1");
                });
            }
            app.UseMvc();

        }
    }
}
