using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class BuyItem
    {
        public int BuyId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}