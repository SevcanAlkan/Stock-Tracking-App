using STA.Core.EntityFramework;
using STA.Core.Enum;
using System.Collections.Generic;

namespace STA.Stock.Model.DTO
{
    public class CustomerBase : Table
    {
        public string Name { get; set; }
        public bool IsInBlackList { get; set; }
        public string Address { get; set; }
        public string Instagram { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
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
