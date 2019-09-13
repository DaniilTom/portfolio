using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Interfaces.Services;
using WebStore.Infrastructure.Implementations;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain;
using WebStore.Interfaces.Api;
using WebStore.ServiceHosting.Controllers;
using WebStore.Clients.Values;
using WebStore.Clients.Employees;
using WebStore.Clients.AllData;
using WebStore.Clients.Users;
using Microsoft.Extensions.Logging;
using WebStore.Logger;
using SmartBreadcrumbs.Extensions;
using WebStore.Hubs;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration conf) { Configuration = conf; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(); // добавляет сервисов для работы MVC

            services.AddSignalR();

            services.AddSingleton<IServiceEmployeeData, EmployeesClient>();

            services.AddScoped<IServiceProductData, AllDataClient>();
            services.AddScoped<IServiceCategoryData, AllDataClient>();

            services.AddScoped<IServiceAllData, AllDataClient>();
            services.AddScoped<IServiceCart, CookiesCartService>();
            services.AddDbContext<WebStoreContext>(op => op.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IValueService, ValuesClient>();

            services.AddIdentity<User, IdentityRole>()
                .AddDefaultTokenProviders();

            #region CustomIdentity implementation

            services.AddTransient<IUserStore<User>, UsersClient>();
            services.AddTransient<IUserRoleStore<User>, UsersClient>();
            services.AddTransient<IUserClaimStore<User>, UsersClient>();
            services.AddTransient<IUserPasswordStore<User>, UsersClient>();
            services.AddTransient<IUserTwoFactorStore<User>, UsersClient>();
            services.AddTransient<IUserEmailStore<User>, UsersClient>();
            services.AddTransient<IUserPhoneNumberStore<User>, UsersClient>();
            services.AddTransient<IUserLoginStore<User>, UsersClient>();
            services.AddTransient<IUserLockoutStore<User>, UsersClient>();

            services.AddTransient<IRoleStore<IdentityRole>, RolesClient>();

            #endregion

            services.Configure<IdentityOptions>(cfg =>
            {
                cfg.Password.RequiredLength = 3;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;

                cfg.Lockout.MaxFailedAccessAttempts = 10;
                cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                cfg.Lockout.AllowedForNewUsers = true;
            });

            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.Cookie.HttpOnly = true;
                cfg.Cookie.Expiration = TimeSpan.FromDays(150);
                cfg.Cookie.MaxAge = TimeSpan.FromDays(150);

                cfg.LoginPath = "/Account/Login";   // незарегистрированный пользователь требует доступ к особому ресурсу
                cfg.LogoutPath = "/Account/Logout";  // пользователь вышел
                cfg.AccessDeniedPath = "/Account/AccessDenied"; //если доступ запрещен

                cfg.SlidingExpiration = true;
            });

            services.AddBreadcrumbs(GetType().Assembly, option =>
            {
                option.TagName = "nav";
                option.SeparatorElement = "<li class=\"separator\">&nbsp;&rarr;&nbsp;</li>";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory log)
        {
            log.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSignalR(routes => {
                routes.MapHub<InformationHub>("/info");
            });

            app.UseMvc(route =>
            {
                route.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                route.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"); // ? - не обязательно, = - по умолчанию
            });
        }
    }
}
