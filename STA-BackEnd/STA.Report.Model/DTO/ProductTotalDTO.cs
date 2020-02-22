using STA.Core.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace STA.Report.Model.DTO
{
    public class ProductTotalDTO : BaseBson
    {
        public Guid ProductId { get; set; }
        public int SellAmount { get; set; }
        public double TotalIncome { get; set; }
    }
}
