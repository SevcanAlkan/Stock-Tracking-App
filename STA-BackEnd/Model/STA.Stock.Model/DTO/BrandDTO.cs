using STA.Core.EntityFramework;
using System.Collections.Generic;

namespace STA.Stock.Model.DTO
{
    public class BrandBase : Table
    {
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
