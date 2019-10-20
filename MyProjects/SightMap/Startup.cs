using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure.Implementations.Test;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.DAL;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;

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

            services.AddDbContext<DataDbContext>(options => options.UseSqlServer(config["ConnectionString"]));

            //services.AddScoped<IDataAccess<SightDTO, ShortSightDTO, SightFilter>, SightsDbAccess>();
            //services.AddScoped<IDataAccess<SightTypeDTO, SightTypeDTO, SightTypeFilter>, SightTypesDbAccess>();

            services.AddScoped<IDataAccess<SightDTO, ShortSightDTO, SightFilterDTO>, SightsDbAccess>();
            services.AddScoped<IDataAccess<SightTypeDTO, SightTypeDTO, SightTypeFilterDTO>, SightTypesDbAccess>();

            // оставил здесь, чтоб пользовать встроенным DI и не подключать сторонний
            // но можно и без DI, переместив создание прямо в SightsDbAccess и SightTypesDbAccess + меньше ссылок на DAL
            services.AddScoped<IRepository<Sight>, SightRepo>();
            services.AddScoped<IRepository<SightType>, SightTypeRepo>();
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
