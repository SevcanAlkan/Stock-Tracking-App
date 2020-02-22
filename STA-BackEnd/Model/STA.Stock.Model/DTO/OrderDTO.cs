using STA.Core.EntityFramework;
using STA.Core.Enum;
using System;

namespace STA.Stock.Model.DTO
{
    public class OrderBase : Table
    {
        public Guid CustomerId { get; set; }
        public Guid ProductStockId { get; set; }
        public double OrderPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime CompleteDateTime { get; set; }
    }

    public class OrderDTO : OrderBase
    {

        //Foreign Keys...
        public virtual CustomerDTO Customer { get; set; }
        public virtual ProductStockDTO ProductStock { get; set; }

        public OrderDTO()
        {

        }
    }
}
