using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using SportsStore.Infrastructure;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository repository;
        private readonly Cart cart;

        public CartController(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl ?? "/"
            });
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, string returnUrl)
        {
            // Find the product
            Product? product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                // Add to cart
                cart.AddItem(product, 1);

                // Debug information - you can remove this in production
                TempData["CartDebug"] = $"Added product {product.Name} to cart. Cart now has {cart.Lines.Count()} items.";
            }

            // Redirect to cart page
            return RedirectToAction("Index", new { returnUrl = returnUrl ?? "/" });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product? product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl = returnUrl ?? "/" });
        }

        [HttpPost]
        public IActionResult UpdateCart(int productId, int quantity, string returnUrl)
        {
            Product? product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.UpdateQuantity(product, quantity);
            }

            return RedirectToAction("Index", new { returnUrl = returnUrl ?? "/" });
        }
    }
}