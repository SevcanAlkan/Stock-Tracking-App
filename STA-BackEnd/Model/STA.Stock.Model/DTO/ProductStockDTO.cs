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

    public class ProductStockDTO : ProductStockBase
    {


        //Foreign Keys...
        public virtual ProductDTO Product { get; set; }
        public virtual ICollection<OrderDTO> Orders { get; set; }

        public ProductStockDTO()
        {
            Orders = new HashSet<OrderDTO>();
        }
    }
}
