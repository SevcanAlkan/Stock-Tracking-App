using STA.Core.Enum;
using STA.Core.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace STA.Report.Model.DTO
{
    public class SystemLog : BaseBson
    {
        public DateTime DateTime { get; set; }
        public string Content { get; set; }
        public Guid RecId { get; set; }
        public RefTable Table { get; set; }
    }
}
