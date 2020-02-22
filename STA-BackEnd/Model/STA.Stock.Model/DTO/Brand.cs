using STA.Core.EntityFramework;
using System.Collections.Generic;

namespace STA.Stock.Model.DTO
{
    public class BrandBase : Table
    {
        public string Name { get; set; }
    }

    public class Brand : BrandBase
    {

        //Foreign Keys...
        public virtual ICollection<Model> Models { get; set; }
        public Brand()
        {
            Models = new HashSet<Model>();
        }
    }
}
