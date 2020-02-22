using STA.Core.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using STA.Core.Validation;

namespace STA.Stock.Model.DTO
{
    public class BrandBase : Table
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
    }

    public class BrandDTO : BrandBase
    {

        //Foreign Keys...
        public virtual ICollection<ModelDTO> Models { get; set; }
        public BrandDTO()
        {
            Models = new HashSet<ModelDTO>();
        }
    }
}
