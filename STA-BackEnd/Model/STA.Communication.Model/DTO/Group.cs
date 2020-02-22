using STA.Core.EntityFramework;
using STA.Core.MongoDB;
using System.Collections.Generic;

namespace STA.Communication.Model.DTO
{
    public class Group : BaseBson
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsOneToOneChat { get; set; }
    }
}
