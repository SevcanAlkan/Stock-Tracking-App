using STA.Core.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace STA.Communication.Model.DTO
{
    public class WallMessage : BaseBson
    {
        public DateTime SentDateTime { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
    }
}
