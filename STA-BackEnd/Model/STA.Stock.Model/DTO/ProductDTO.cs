using STA.Core.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using STA.Core.Validation;

namespace STA.Stock.Model.DTO
{
    public class ProductBase : Table
    {
        [GuidValidation]
        public Guid ModelId { get; set; }
        [GuidValidation]
        public Guid ColorId { get; set; }
        [MaxLength(20)]
        public string NumberRange { get; set; } // Going to be filled by system, depending to the ProductStock table.
        [MaxLength(100)]
        public string PictureId { get; set; } // Use picture of the model or overwrite it here.
        [Required, Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int StockCount { get; set; } // From ProductStock
    }

    public class ProductDTO : ProductBase
    {

        //Foreign Keys...
        public virtual ModelDTO Model { get; set; }
        public virtual ColorDTO Color { get; set; }

        public virtual ICollection<ProductStockDTO> Stock { get; set; }

        public ProductDTO()
        {
            Stock = new HashSet<ProductStockDTO>();
        }
    }
}
