using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int MadePercent { get; set; }

        [Required]
        public int RestrauntId { get; set; }

        [Required]
        public int ToPay { get; set; }

        [Required]
        public string OrdererItemsAndCountJson { get; set; }
    }
}
