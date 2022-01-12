using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class ProductTags
    {
        public int Id { get; set; }
        [Display(Name = "Product Tag")]
        public string Name { get; set; }
    }
}
