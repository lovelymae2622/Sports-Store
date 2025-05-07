﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please specify a category")]
        public string Category { get; set; } = string.Empty;

        private string imageUrl = "/img/placeholder.jpg";

        public string ImageUrl
        {
            get => imageUrl;
            set => imageUrl = string.IsNullOrEmpty(value) ? "/img/placeholder.jpg" : value;
        }
    }
}