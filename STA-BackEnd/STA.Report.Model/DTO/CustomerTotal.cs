using STA.Core.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace STA.Report.Model.DTO
{
    public class CustomerTotal : BaseBson
    {
        public Guid CustomerId { get; set; }
        public int OrderAmount { get; set; }
        public double TotalMoneySpent { get; set; }
    }
}
