using STA.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace STA.Stock.Model.DTO
{
    public class ColorBase : Table
    {
        public string Name { get; set; }
        public string Code { get; set; } // Hex or RGB
    }

    public class Color : ColorBase
    {

        //Foreign Keys...
        public virtual ICollection<Product> Products { get; set; }
        public Color()
        {
            Products = new HashSet<Product>();
        }
    }
}
