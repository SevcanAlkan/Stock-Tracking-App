using STA.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using STA.Core.Validation;

namespace STA.Stock.Model.DTO
{
    public class ModelBase : Table
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [GuidValidation]
        public Guid BrandId { get; set; }
        [MaxLength(100)]
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
