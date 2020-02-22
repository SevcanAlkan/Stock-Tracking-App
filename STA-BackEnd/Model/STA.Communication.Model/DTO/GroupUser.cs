using STA.Core.EntityFramework;
using STA.Core.MongoDB;
using System;

namespace STA.Communication.Model.DTO
{
    public class GroupUser : BaseBson
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }
}
