using STA.Core.EntityFramework;
using STA.Core.Enum;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using STA.Core.Validation;

namespace STA.Stock.Model.DTO
{
    public class InfuluencerBase : Table
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string Instagram { get; set; }
        [MaxLength(50)]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [Required, DefaultValue(Gender.Unknown)]
        public Gender Gender { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Range(0, int.MaxValue)]
        public int Followers { get; set; }
    }

    public class InfuluencerDTO : InfuluencerBase
    {


        //Foreign Keys...
        public virtual ICollection<AdCampainDTO> Campains { get; set; }

        public InfuluencerDTO()
        {
            Campains = new HashSet<AdCampainDTO>();
        }
    }
}
