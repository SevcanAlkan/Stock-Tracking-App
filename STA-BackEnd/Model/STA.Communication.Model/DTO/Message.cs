using STA.Core.EntityFramework;
using STA.Core.MongoDB;
using System;

namespace STA.Communication.Model.DTO
{
    public class Message : BaseBson
    {
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public Guid? ToUserId { get; set; }
    }
}
