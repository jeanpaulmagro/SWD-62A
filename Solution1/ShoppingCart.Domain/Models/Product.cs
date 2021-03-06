﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Dynamic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Domain.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        [ForeignKey("Category")]

        public int CategoryId { get; set; }



    }
}
