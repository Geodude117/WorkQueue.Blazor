using DomainData.BusinessLogic.DomainGroup;
using DomainData.BusinessLogic.DomainInformation;
using DomainData.BusinessLogic.DomainType;
using DomainData.BusinessLogic.QuestionViewModel;
using DomainData.Repository.DomainGroupRepo;
using DomainData.Repository.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;

namespace DomainData.API
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
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();

            //services.AddTransient<IUnitOfWork>(connection => new UnitOfWork(Configuration.GetConnectionString("DbConnection")));
            services.AddTransient<IUnitOfWork>(connection => new UnitOfWork(Configuration.GetConnectionString("DbConnection")));
            services.AddTransient<IDomainGroupBusiness, DomainGroupBusiness>();
            services.AddTransient<IDomainInformationBusiness, DomainInformationBusiness>();
            services.AddTransient<IDomainTypeBusiness, DomainTypeBusiness>();
            services.AddTransient<IQuestionBusiness, QuestionBusiness>();

            services.AddSingleton<IConfiguration>(x => Configuration);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
