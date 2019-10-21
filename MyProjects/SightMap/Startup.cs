using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SightMap.BLL;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure.Implementations;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.BLL.Mappers;

namespace SightMap
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        private IConfiguration config;

        public Startup(IConfiguration _config) => config = _config;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);

            //services.AddScoped<IDataAccess<SightDTO, ShortSightDTO, SightFilter>, SightsDbAccess>();
            //services.AddScoped<IDataAccess<SightTypeDTO, SightTypeDTO, SightTypeFilter>, SightTypesDbAccess>();

            services.AddScoped<IDbManager<SightDTO, ShortSightDTO, SightFilterDTO>, SightsDbManager>();
            services.AddScoped<IDbManager<SightTypeDTO, SightTypeDTO, SightTypeFilterDTO>, SightTypesDbManager>();

            var mappingConfig = MapperConfig.Initialize();
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddBLLFunctional(config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("LogInformation in startup.cs");
            logger.LogDebug("LogDebug in startup.cs");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc();

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
