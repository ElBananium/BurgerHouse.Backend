﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.NonDb.Api
{
    public class ApiOrderReturn
    {
        public int Id { get; set; }
        public int MadePercent { get; set; }

        public int RestrauntId { get; set; }

        public int Price { get; set; }
        public List<OrderedItem> OrderedItems { get; set; }
    }
}
