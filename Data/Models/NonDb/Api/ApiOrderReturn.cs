using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.NonDb.Api
{
    public class ApiOrderReturn
    {
        public int MadePercent { get; set; }

        public List<int> OrderedItems { get; set; }
    }
}
