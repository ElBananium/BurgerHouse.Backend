﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Item
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public string Name { get; set; }


        public string Description { get; set; }

        public int Price { get; set; }
        public string ImgSrc { get; set; }
    }
}