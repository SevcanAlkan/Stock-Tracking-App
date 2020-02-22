using STA.Core.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace STA.Report.Model.DTO
{
    public class ProdcutPriceHistoryDTO : BaseBson
    {
        public Guid ProdcutId { get; set; }
        public DateTime ChangeDate { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public Guid ChangedBy { get; set; }
    }
}
