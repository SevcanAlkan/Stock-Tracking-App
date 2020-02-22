using STA.Core.EntityFramework;
using System;

namespace STA.Stock.Model.DTO
{
    public class Products : Table
    {
        public Guid ModelId { get; set; }
        public Guid ColorId { get; set; }
        public string NumberRange { get; set; } // Going to be filled by system, depending to the ProductStock table.
        public string PictureId { get; set; } // Use picture of the model or overwrite it here.
        public double Price { get; set; }
        public int StockCount { get; set; } // From ProductStock
    }
}
