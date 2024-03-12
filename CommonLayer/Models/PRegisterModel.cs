using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Models
{
    public class PRegisterModel
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
    }
}
