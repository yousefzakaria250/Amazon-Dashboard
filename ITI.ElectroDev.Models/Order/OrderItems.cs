using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ElectroDev.Models
{
    public class OrderItems
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; } 
        public double Price { get; set; }
        public virtual OrderDetails OrderDetails { get; set; }
        public virtual Product Product { get; set; }
    }
}
