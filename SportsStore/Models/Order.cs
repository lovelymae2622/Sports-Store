using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportsStore.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the first address line")]
        [Display(Name = "Address Line 1")]
        public string Line1 { get; set; } = string.Empty;

        [Display(Name = "Address Line 2")]
        public string Line2 { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a province")]
        public string Province { get; set; } = string.Empty;

        public string Zip { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a country")]
        public string Country { get; set; } = string.Empty;

        public bool GiftWrap { get; set; }

        [BindNever]
        public bool Shipped { get; set; }
    }
}