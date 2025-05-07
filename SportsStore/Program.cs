using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Configure for MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("SportsStoreConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SportsStoreConnection"))
    ));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.MapControllerRoute(
    name: "categoryPage",
    pattern: "{category}/Page{productPage:int}",
    defaults: new { Controller = "Product", action = "List" });

app.MapControllerRoute(
    name: "page",
    pattern: "Page{productPage:int}",
    defaults: new { Controller = "Product", action = "List", productPage = 1 });

app.MapControllerRoute(
    name: "category",
    pattern: "{category}",
    defaults: new { Controller = "Product", action = "List", productPage = 1 });

app.MapControllerRoute(
    name: "pagination",
    pattern: "Products/Page{productPage}",
    defaults: new { Controller = "Product", action = "List", productPage = 1 });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=List}/{id?}");

SeedData.EnsurePopulated(app);

app.Run();