using STA.Core.EntityFramework;
using STA.Core.Enum;
using System.Collections.Generic;

namespace STA.Stock.Model.DTO
{
    public class InfuluencerBase : Table
    {
        public string  Name { get; set; }
        public string Address { get; set; }
        public string Instagram { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Description { get; set; }
        public int Followers { get; set; }
    }

    public class Infuluencer : InfuluencerBase
    {


        //Foreign Keys...
        public virtual ICollection<AdCampain> Campains { get; set; }

        public Infuluencer()
        {
            Campains = new HashSet<AdCampain>();
        }
    }
}
