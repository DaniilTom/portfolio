using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SightMap.BLL.DTO;
using SightMap.DAL;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;

namespace SightMap.BLL
{
    public static class Configuration
    {
        public static IServiceCollection AddBLLFunctional(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<DataDbContext>(options => options.UseSqlServer(config["ConnectionString"]));

            services.AddScoped<IRepository<Sight>, SightRepo>();
            services.AddScoped<IRepository<SightType>, SightTypeRepo>();

            return services;
        }
    }
}
