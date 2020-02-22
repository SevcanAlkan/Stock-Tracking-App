using STA.Core.EntityFramework;
using STA.Core.Enum;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using STA.Core.Validation;

namespace STA.Stock.Model.DTO
{
    public class CustomerBase : Table
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [DefaultValue(false)]
        public bool IsInBlackList { get; set; }
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
    }

    public class CustomerDTO : CustomerBase
    {


        //Foreign Keys...
        public virtual ICollection<OrderDTO> Orders { get; set; }

        public CustomerDTO()
        {
            Orders = new HashSet<OrderDTO>();
        }
    }
}
