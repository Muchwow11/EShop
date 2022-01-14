using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }

        public int Id { get; set; }
        [Display(Name = "Order Nr")]
        public string OrderNr { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name="Phone Nr")]
        public string PhoneNr { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
