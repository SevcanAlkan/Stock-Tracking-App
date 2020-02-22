using STA.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using STA.Core.Validation;

namespace STA.Stock.Model.DTO
{
    public class ColorBase : Table
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(20)]
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
