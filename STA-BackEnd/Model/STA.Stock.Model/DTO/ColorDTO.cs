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

    public class ColorDTO : ColorBase
    {

        //Foreign Keys...
        public virtual ICollection<ProductDTO> Products { get; set; }
        public ColorDTO()
        {
            Products = new HashSet<ProductDTO>();
        }
    }
}
