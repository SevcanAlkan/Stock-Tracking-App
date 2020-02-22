using STA.Core.EntityFramework;
using STA.Core.Enum;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using STA.Core.Validation;

namespace STA.Stock.Model.DTO
{
    public class OrderBase : Table
    {
        [GuidValidation]
        public Guid CustomerId { get; set; }
        [GuidValidation]
        public Guid ProductStockId { get; set; }
        [Required, Range(0, double.MaxValue)]
        public double OrderPrice { get; set; }
        [Required, DefaultValue(OrderStatus.Created)]
        public OrderStatus Status { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public DateTime? CompleteDateTime { get; set; }
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
