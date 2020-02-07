using NGA.Core.EntityFramework;
using System;

namespace NGA.Domain
{
    public class GroupUserBase : Base
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }

    public class GroupUser : GroupUserBase
    {
        //Foreign keys
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }

        public GroupUser()
        {
        }
    }
}
