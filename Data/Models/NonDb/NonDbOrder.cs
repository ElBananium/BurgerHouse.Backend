using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.NonDb
{
    public class NonDbOrder
    {
        public int RestrauntId { get; set; }

        public List<OrderedItem> OrderedItems { get; set; }
    }
}
