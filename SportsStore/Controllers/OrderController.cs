using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository repository;
        private readonly Cart cart;

        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }

        public ViewResult Checkout()
        {
            // Create a list of countries for the dropdown
            ViewBag.Countries = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select a country", Value = "", Selected = true },
                new SelectListItem { Text = "Philippines", Value = "Philippines" },
                new SelectListItem { Text = "United States", Value = "United States" },
                new SelectListItem { Text = "Canada", Value = "Canada" },
                new SelectListItem { Text = "United Kingdom", Value = "United Kingdom" },
                new SelectListItem { Text = "Australia", Value = "Australia" },
                new SelectListItem { Text = "Japan", Value = "Japan" },
                new SelectListItem { Text = "China", Value = "China" },
                new SelectListItem { Text = "Singapore", Value = "Singapore" },
                new SelectListItem { Text = "Malaysia", Value = "Malaysia" },
                new SelectListItem { Text = "Indonesia", Value = "Indonesia" },
                new SelectListItem { Text = "Thailand", Value = "Thailand" },
                new SelectListItem { Text = "Vietnam", Value = "Vietnam" },
                new SelectListItem { Text = "South Korea", Value = "South Korea" },
                new SelectListItem { Text = "India", Value = "India" },
                new SelectListItem { Text = "Germany", Value = "Germany" },
                new SelectListItem { Text = "France", Value = "France" },
                new SelectListItem { Text = "Italy", Value = "Italy" },
                new SelectListItem { Text = "Spain", Value = "Spain" },
                new SelectListItem { Text = "Brazil", Value = "Brazil" },
                new SelectListItem { Text = "Mexico", Value = "Mexico" }
            };

            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!cart.Lines.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                cart.Clear();
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                // If validation fails, we need to repopulate the countries dropdown
                ViewBag.Countries = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Select a country", Value = "", Selected = string.IsNullOrEmpty(order.Country) },
                    new SelectListItem { Text = "Philippines", Value = "Philippines", Selected = order.Country == "Philippines" },
                    new SelectListItem { Text = "United States", Value = "United States", Selected = order.Country == "United States" },
                    new SelectListItem { Text = "Canada", Value = "Canada", Selected = order.Country == "Canada" },
                    new SelectListItem { Text = "United Kingdom", Value = "United Kingdom", Selected = order.Country == "United Kingdom" },
                    new SelectListItem { Text = "Australia", Value = "Australia", Selected = order.Country == "Australia" },
                    new SelectListItem { Text = "Japan", Value = "Japan", Selected = order.Country == "Japan" },
                    new SelectListItem { Text = "China", Value = "China", Selected = order.Country == "China" },
                    new SelectListItem { Text = "Singapore", Value = "Singapore", Selected = order.Country == "Singapore" },
                    new SelectListItem { Text = "Malaysia", Value = "Malaysia", Selected = order.Country == "Malaysia" },
                    new SelectListItem { Text = "Indonesia", Value = "Indonesia", Selected = order.Country == "Indonesia" },
                    new SelectListItem { Text = "Thailand", Value = "Thailand", Selected = order.Country == "Thailand" },
                    new SelectListItem { Text = "Vietnam", Value = "Vietnam", Selected = order.Country == "Vietnam" },
                    new SelectListItem { Text = "South Korea", Value = "South Korea", Selected = order.Country == "South Korea" },
                    new SelectListItem { Text = "India", Value = "India", Selected = order.Country == "India" },
                    new SelectListItem { Text = "Germany", Value = "Germany", Selected = order.Country == "Germany" },
                    new SelectListItem { Text = "France", Value = "France", Selected = order.Country == "France" },
                    new SelectListItem { Text = "Italy", Value = "Italy", Selected = order.Country == "Italy" },
                    new SelectListItem { Text = "Spain", Value = "Spain", Selected = order.Country == "Spain" },
                    new SelectListItem { Text = "Brazil", Value = "Brazil", Selected = order.Country == "Brazil" },
                    new SelectListItem { Text = "Mexico", Value = "Mexico", Selected = order.Country == "Mexico" }
                };
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            return View();
        }
    }
}