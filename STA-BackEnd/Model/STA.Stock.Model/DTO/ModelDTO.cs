using STA.Core.EntityFramework;
using System;
using System.Collections.Generic;

namespace STA.Stock.Model.DTO
{
    public class ModelBase : Table
    {
        public string Name { get; set; }
        public Guid BrandId { get; set; }
        public string PictureId { get; set; }
    }

    public class ModelDTO : ModelBase
    {


        //Foreign Keys...
        public virtual BrandDTO Brand { get; set; }

        public virtual ICollection<ProductDTO> Products { get; set; }

        public ModelDTO()
        {
            Products = new HashSet<ProductDTO>();
        }
    }
}
