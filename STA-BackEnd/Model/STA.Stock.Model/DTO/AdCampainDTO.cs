using STA.Core.EntityFramework;
using STA.Core.Enum;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using STA.Core.Validation;

namespace STA.Stock.Model.DTO
{
    public class AdCampainBase : Table
    {
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required, MaxLength(200)]
        public string Subject { get; set; }
        [Required, MaxLength(500)]
        public string Description { get; set; }
        [Required, DefaultValue(AdStatus.NotStarted)]
        public AdStatus Status { get; set; }
        [Required, Range(0, double.MaxValue)]
        public double Price { get; set; }
        [MaxLength(500)]
        public string CompleteComment { get; set; }
        [GuidValidation]
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
