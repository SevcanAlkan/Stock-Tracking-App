using STA.Core.EntityFramework;
using System;
using System.Collections.Generic;

namespace STA.Stock.Model.DTO
{
    public class ProductStockBase : Table
    {
        public Guid ProductId { get; set; }
        public int Number { get; set; }
        public int Amount { get; set; }
    }

    public class ProductStock : ProductStockBase
    {


        //Foreign Keys...
        public virtual Product Product { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public ProductStock()
        {
            Orders = new HashSet<Order>();
        }
    }
}
