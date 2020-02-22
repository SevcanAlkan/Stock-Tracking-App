using STA.Core.EntityFramework;
using STA.Core.Enum;
using System;

namespace STA.Stock.Model.DTO
{
    public class OrderBase : Table
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductStockId { get; set; }
        public double OrderPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime CompleteDateTime { get; set; }
    }

    public class Order : OrderBase
    {

        //Foreign Keys...
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductStock ProductStock { get; set; }

        public Order()
        {

        }
    }
}
