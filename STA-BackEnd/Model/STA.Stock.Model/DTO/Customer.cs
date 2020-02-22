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

    public class Customer : CustomerBase
    {


        //Foreign Keys...
        public virtual ICollection<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new HashSet<Order>();
        }
    }
}
