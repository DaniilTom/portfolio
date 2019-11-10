using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SightMap.BLL;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure.Implementations;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.BLL.Mappers;
using System.Globalization;

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

            services.AddScoped<IBaseManager<SightDTO, SightFilterDTO>, SightsDbManager>();
            services.AddScoped<IBaseManager<SightTypeDTO, SightTypeFilterDTO>, SightTypesDbManager>();
            services.AddScoped<IBaseManager<ReviewDTO, ReviewFilterDTO>, ReviewsDbManager>();
            services.AddScoped<IBaseManager<AlbumDTO, AlbumFilterDTO>, AlbumDbManager>();

            var mappingConfig = MapperConfig.Initialize();
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddBLLManagment(config);

            services.AddMemoryCache();

            services.AddCors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseCors(builder => builder.AllowAnyOrigin());
                //app.UseSpa(config =>
                //{
                //    config.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                //});
            }

            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseStaticFiles();

            //app.UseSpa(config =>
            //{
            //    config.UseProxyToSpaDevelopmentServer("http://localhost:4200");
            //});

            app.UseMvc(route =>
            {
                route.MapRoute(
                    name: "Api",
                    template: "api/{controller}"
                  );

                route.MapRoute(
                    name: "default",
                    template: "{controller=Angular}/{action=Index}"
                  );
            });
        }
    }
}
