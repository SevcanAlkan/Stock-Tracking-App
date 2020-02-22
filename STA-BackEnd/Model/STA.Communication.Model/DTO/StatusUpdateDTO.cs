using STA.Core.Enum;
using STA.Core.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace STA.Communication.Model.DTO
{
    public class StatusUpdateDTO : BaseBson
    {
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateTime { get; set; }
        public Guid RecId { get; set; }
        public RefTable Table { get; set; }
    }
}
