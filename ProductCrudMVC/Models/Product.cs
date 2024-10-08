﻿using System.ComponentModel.DataAnnotations;

namespace ProductCrudMVC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Createdat { get; set; } = DateTime.Now;
        public Category? Category { get; set; }
    }
}
