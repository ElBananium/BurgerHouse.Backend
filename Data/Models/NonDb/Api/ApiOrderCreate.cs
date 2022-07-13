using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.NonDb.Api
{
    public class ApiOrderCreate
    {
        public int RestrauntId { get; set; }

        public List<int> ItemsIds { get; set; }
    }
}
