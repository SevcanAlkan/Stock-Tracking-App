using STA.Core.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace STA.Stock.Model.DTO
{
    public class ProductBase : Table
    {
        public Guid ModelId { get; set; }
        public Guid ColorId { get; set; }
        public string NumberRange { get; set; } // Going to be filled by system, depending to the ProductStock table.
        public string PictureId { get; set; } // Use picture of the model or overwrite it here.
        public double Price { get; set; }
        public int StockCount { get; set; } // From ProductStock
    }

    public class ProductDTO : ProductBase
    {

        //Foreign Keys...
        public virtual ModelDTO Model { get; set; }
        public virtual ColorDTO  Color { get; set; }

        public virtual ICollection<ProductStockDTO> Stock { get; set; }

        public ProductDTO()
        {
            Stock = new HashSet<ProductStockDTO>();
        }
    }
}
