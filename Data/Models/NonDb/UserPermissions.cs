using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.NonDb
{
    public class UserPermissions
    {
        public int UserId { get; set; }

        public bool CanViewOrders { get; set; }

        public bool CanMakeOrders { get; set; }
    }
}
