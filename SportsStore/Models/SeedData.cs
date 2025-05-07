using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Basketball",
                        Description = "NBA-sized basketball",
                        Category = "Basketball",
                        Price = 2305.00M,
                        ImageUrl = "/img/basketball.jpg"
                    },
                    new Product
                    {
                        Name = "Soccer Ball",
                        Description = "FIFA-approved size and weight",
                        Category = "Soccer",
                        Price = 1950.50M,
                        ImageUrl = "/img/soccer.jpg"
                    },
                    new Product
                    {
                        Name = "Tennis Racket",
                        Description = "Professional tennis racket",
                        Category = "Tennis",
                        Price = 3500.00M,
                        ImageUrl = "/img/tennis.jpg"
                    },
                    new Product
                    {
                        Name = "Running Shoes",
                        Description = "Lightweight running shoes",
                        Category = "Running",
                        Price = 4250.00M,
                        ImageUrl = "/img/running-shoes.jpg"
                    },
                    new Product
                    {
                        Name = "Yoga Mat",
                        Description = "Non-slip yoga mat with carrying strap",
                        Category = "Fitness",
                        Price = 850.00M,
                        ImageUrl = "/img/yoga-mat.jpg"
                    },
                    new Product
                    {
                        Name = "Dumbbells Set",
                        Description = "Set of 2 adjustable dumbbells (2-10kg each)",
                        Category = "Fitness",
                        Price = 4200.00M,
                        ImageUrl = "/img/dumbbells.jpg"
                    },
                    new Product
                    {
                        Name = "Swimming Goggles",
                        Description = "Anti-fog swimming goggles with UV protection",
                        Category = "Swimming",
                        Price = 550.00M,
                        ImageUrl = "/img/goggles.jpg"
                    },
                    new Product
                    {
                        Name = "Badminton Set",
                        Description = "Complete badminton set with 2 rackets and shuttlecocks",
                        Category = "Racket Sports",
                        Price = 1800.00M,
                        ImageUrl = "/img/badminton.jpg"
                    },
                    new Product
                    {
                        Name = "Baseball Bat",
                        Description = "Professional wooden baseball bat",
                        Category = "Baseball",
                        Price = 2750.00M,
                        ImageUrl = "/img/baseball-bat.jpg"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}