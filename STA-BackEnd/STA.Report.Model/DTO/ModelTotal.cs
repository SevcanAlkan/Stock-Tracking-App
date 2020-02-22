using STA.Core.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace STA.Report.Model.DTO
{
    public class ModelTotal : BaseBson
    {
        public int SoldAmount { get; set; }
        public double TotalIncome { get; set; }
    }
}
