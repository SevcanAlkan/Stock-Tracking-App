using STA.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using STA.Core.Validation;

namespace STA.Stock.Model.DTO
{
    public class ProductStockBase : Table
    {
        [GuidValidation]
        public Guid ProductId { get; set; }
        [Required, Range(0, 100)]
        public int Number { get; set; }
        [Required, Range(0, int.MaxValue)]
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
