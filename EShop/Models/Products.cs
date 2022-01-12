using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class Products
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Image { get; set; }
        [Display(Name = "Color")]
        public string Color { get; set; }
        [Required]
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }
        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Categories Categories { get; set; }
        [Display(Name = "Product Tag")]
        [Required]
        public int ProductTagId { get; set; }
        [ForeignKey("ProductTagId")]
        public ProductTags ProductTags { get; set; }
    }
}
