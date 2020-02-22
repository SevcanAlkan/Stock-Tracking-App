using STA.Core.EntityFramework;
using STA.Core.Enum;
using System;

namespace STA.Stock.Model.DTO
{
    public class AdCampainBase : Table
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public AdStatus Status { get; set; }
        public double Price { get; set; }
        public string CompleteComment { get; set; }
        public Guid InfuluencerId { get; set; }
    }

    public class AdCampainDTO : AdCampainBase
    {


        //Foreign Keys...
        public virtual InfuluencerDTO Infuluencer { get; set; }

        public AdCampainDTO()
        {

        }
    }
}
