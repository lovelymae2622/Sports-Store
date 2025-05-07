using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;
using System;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace SportsStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("MySqlConnection"),
                    new MySqlServerVersion(new Version(8, 0, 21))));

            services.AddScoped<IProductRepository, EFProductRepository>();
            services.AddScoped<IOrderRepository, EFOrderRepository>();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // global ₱ currency setting
            var cultureInfo = new CultureInfo("en-PH")
            {
                NumberFormat = { CurrencySymbol = "₱" }
            };
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "categoryPage",
                    pattern: "{category}/Page{page:int}",
                    defaults: new { Controller = "Product", action = "List" });

                endpoints.MapControllerRoute(
                    name: "page",
                    pattern: "Page{page:int}",
                    defaults: new { Controller = "Product", action = "List", page = 1 });

                endpoints.MapControllerRoute(
                    name: "category",
                    pattern: "{category}",
                    defaults: new { Controller = "Product", action = "List", page = 1 });

                endpoints.MapControllerRoute(
                    name: "pagination",
                    pattern: "Products/Page{page}",
                    defaults: new { Controller = "Product", action = "List", page = 1 });

                endpoints.MapDefaultControllerRoute();
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
