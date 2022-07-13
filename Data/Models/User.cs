using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class User
    {

        public int Id { get; set; }

        [Phone]
        public string MobilePhone { get; set; }

        public bool IsConfirmed { get; set; }


        public bool IsWorker { get; set; }

        public bool IsAdmin { get; set; }
    }
}
