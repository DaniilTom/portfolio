using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SightMap.BLL.CustomCache;
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

            services.AddDbContext<DataDbContext>(options => options.UseSqlServer(config["ConnectionString"]), ServiceLifetime.Singleton);

            services.AddScoped<IRepository<Sight>, SightRepo>();
            services.AddScoped<IRepository<SightType>, SightTypeRepo>();
            services.AddScoped<IRepository<Review>, ReviewRepo>();

            //services.AddScoped<ICustomCache<SightDTO>, SlidingCustomCache<SightDTO>>();
            //services.AddScoped<ICustomCache<SightTypeDTO>, AbsoluteCustomCache<SightTypeDTO>>();
            //services.AddScoped<ICustomCache<ReviewDTO>, AbsoluteCustomCache<ReviewDTO>>();

            services.AddSingleton<ICustomCache, CustomCache.CustomCache>();

            return services;
        }
    }
}
