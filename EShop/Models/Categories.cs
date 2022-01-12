using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class Categories
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Category")]
        public string Category { get; set; }

    }
}
